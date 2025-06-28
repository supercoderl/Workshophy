using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WorkshopHub.Domain.DomainEvents;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Infrastructure.Database;
using WorkshopHub.Infrastructure.EventSourcing;
using WorkshopHub.Infrastructure.Repositories;

namespace WorkshopHub.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string migrationsAssemblyName,
        string connectionString)
        {
            // Add event store db context
            services.AddDbContext<EventStoreDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        connectionString,
                        b => b.MigrationsAssembly(migrationsAssemblyName));
                });

            services.AddDbContext<DomainNotificationStoreDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        connectionString,
                        b => b.MigrationsAssembly(migrationsAssemblyName));
                });

            // Core Infra
            services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
            services.AddScoped<IEventStoreContext, EventStoreContext>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IDomainEventStore, DomainEventStore>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IBadgeRepository, BadgeRepository>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<IRecommendationRepository, RecommendationRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IUserBadgeRepository, UserBadgeRepository>();
            services.AddScoped<IUserInterestRepository, UserInterestRepository>();
            services.AddScoped<IWorkshopPromotionRepository, WorkshopPromotionRepository>();
            services.AddScoped<IWorkshopRepository, WorkshopRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IActivityPointRuleRepository, ActivityPointRuleRepository>();

            return services;
        }
    }
}
