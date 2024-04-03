using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Services.Services;

public class BackgroundServiceSuspension : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BackgroundServiceSuspension> _logger;

    public BackgroundServiceSuspension(IServiceScopeFactory scopeFactory, ILogger<BackgroundServiceSuspension> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var updateSuspensionService = scope.ServiceProvider.GetRequiredService<IUpdateSuspensionService>();
                    var context = scope.ServiceProvider.GetRequiredService<HitchContext>();
                    await updateSuspensionService.UpdateSuspensionDue(context);
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