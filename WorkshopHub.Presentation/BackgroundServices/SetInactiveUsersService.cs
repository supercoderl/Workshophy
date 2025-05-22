using Microsoft.EntityFrameworkCore;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Enums;
using WorkshopHub.Infrastructure.Database;

namespace WorkshopHub.Presentation.BackgroundServices
{
    public sealed class SetInactiveUsersService : BackgroundService
    {
        private readonly ILogger<SetInactiveUsersService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public SetInactiveUsersService(
            IServiceProvider serviceProvider,
            ILogger<SetInactiveUsersService> logger)
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

                IList<User> inactiveUsers = Array.Empty<User>();

                try
                {
                    var cutoffDate = DateTimeOffset.UtcNow.AddDays(-30);

                    inactiveUsers = await context.Users
                        .Where(user =>
                            user.LastLoggedinDate < cutoffDate &&
                            user.Status == UserStatus.Active)
                        .Take(250)
                        .ToListAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while retrieving users to set inactive");
                }

                foreach (var user in inactiveUsers)
                {
                    user.SetStatus(UserStatus.Inactive);
                }

                try
                {
                    await context.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while setting users to inactive");
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
