using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using pagyeonjaAPI.Entities;

public class UpdateSuspensionService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<UpdateSuspensionService> _logger;

    public UpdateSuspensionService(IServiceScopeFactory scopeFactory, ILogger<UpdateSuspensionService> logger)
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
                    var context = scope.ServiceProvider.GetRequiredService<HitchContext>();
                    UpdateSuspensionDue(context);
                }

                _logger.LogInformation("UpdateSuspensionService ran at: {time}", DateTimeOffset.Now);

                var now = DateTime.Now;
                var nextRunTime = now.AddHours(1);
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

    private void UpdateSuspensionDue(HitchContext context)
    {
        try
        {
            var suspendedData = context.Suspensions.Where(s => s.SuspensionDate < DateTime.Now).ToList();
            foreach (var sd in suspendedData)
            {
                if (sd.UserType == "Commuter")
                {
                    //query for commuter
                    var filteredCommuter = context.Commuters.Where(c => c.CommuterId == sd.UserId && sd.UserType == "Commuter" && c.SuspensionStatus == true).FirstOrDefault();
                    if (filteredCommuter != null)
                    {
                        filteredCommuter.SuspensionStatus = false;
                    }
                }
                else if (sd.UserType == "Rider")
                {
                    //query for rider
                    var filteredRider = context.Riders.Where(r => r.RiderId == sd.UserId && sd.UserType == "Rider" && r.SuspensionStatus == true).FirstOrDefault();
                    if (filteredRider != null)
                    {
                        filteredRider.SuspensionStatus = false;
                    }
                }
            }
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Log the error details
            _logger.LogError(ex, "An error occurred while updating DaysDue.");
        }
    }

}