using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Services.Services;

public class UpdateSuspensionServiceController : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<UpdateSuspensionServiceController> _logger;
    private readonly IUpdateSuspensionService _updateSuspensionService;
    public UpdateSuspensionServiceController(IServiceScopeFactory scopeFactory, ILogger<UpdateSuspensionServiceController> logger, IUpdateSuspensionService updateSuspensionService)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _updateSuspensionService = updateSuspensionService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<HitchContext>();
                    await _updateSuspensionService.UpdateSuspensionDue(context);
                }

                _logger.LogInformation("UpdateSuspensionService ran at: {time}", DateTimeOffset.Now);

                var now = DateTime.Now;
                var nextRunTime = now.AddDays(1).AddHours(1); //Runtime every day 1 AM
                var delay = nextRunTime - now;

                if (delay > TimeSpan.Zero)
                {
                    await Task.Delay(delay, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                // Log the error details
                _logger.LogError(ex, "An error occurred in ExecuteAsync.");
            }
        }
    }
}