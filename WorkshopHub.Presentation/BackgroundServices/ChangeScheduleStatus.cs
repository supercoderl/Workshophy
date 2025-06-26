using Microsoft.EntityFrameworkCore;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Enums;
using WorkshopHub.Domain.Helpers;
using WorkshopHub.Infrastructure.Database;

namespace WorkshopHub.Presentation.BackgroundServices
{
    public sealed class ChangeScheduleStatus : BackgroundService
    {
        private readonly ILogger<ChangeScheduleStatus> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ChangeScheduleStatus(
            IServiceProvider serviceProvider,
            ILogger<ChangeScheduleStatus> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                IList<WorkshopSchedule> schedules = Array.Empty<WorkshopSchedule>();

                var now = TimeHelper.GetTimeNow();
                try
                {
                    schedules = await context.WorkshopSchedules
                        .Where(s => now > s.StartTime)
                        .Take(250)
                        .ToListAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while retrieving schedules to set status");
                }

                foreach (var schedule in schedules)
                {
                    if(now > schedule.EndTime && schedule.ScheduleStatus != ScheduleStatus.Ended)
                    {
                        schedule.SetScheduleStatus(ScheduleStatus.Ended);
                    }
                    else if(now < schedule.EndTime && schedule.ScheduleStatus != ScheduleStatus.Starting)
                    {
                        schedule.SetScheduleStatus(ScheduleStatus.Starting);
                    }
                }

                try
                {
                    await context.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while setting schedules");
                }

                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
        }
    }
}
