using Microsoft.EntityFrameworkCore;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Infrastructure.Configurations;

namespace WorkshopHub.Infrastructure.Database
{
    public partial class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public DbSet<Badge> Badges { get; set; } = null!;
        public DbSet<BlogPost> BlogsPosts { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Promotion> Promotions { get; set; } = null!;
        public DbSet<Recommendation> Recommendations { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<UserBadge> UserBadges { get; set; } = null!;
        public DbSet<UserInterest> UserInterests { get; set; } = null!;
        public DbSet<Workshop> Workshops { get; set; } = null!;
        public DbSet<WorkshopPromotion> WorkshopPromotions { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<ActivityPointRule> ActivityPointRules { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                if (entity.ClrType.GetProperty(DbContextUtility.IsDeletedProperty) is not null)
                {
                    builder.Entity(entity.ClrType)
                        .HasQueryFilter(DbContextUtility.GetIsDeletedRestriction(entity.ClrType));
                }
            }

            base.OnModelCreating(builder);

            ApplyConfigurations(builder);

            // Make referential delete behaviour restrict instead of cascade for everything
            foreach (var relationship in builder.Model.GetEntityTypes()
                         .SelectMany(x => x.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private static void ApplyConfigurations(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new WorkshopConfiguration());
            builder.ApplyConfiguration(new BadgeConfiguration());
            builder.ApplyConfiguration(new BlogPostConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new PromotionConfiguration());
            builder.ApplyConfiguration(new RecommendationConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());
            builder.ApplyConfiguration(new TicketConfiguration());
            builder.ApplyConfiguration(new UserBadgeConfiguration());
            builder.ApplyConfiguration(new UserInterestConfiguration());
            builder.ApplyConfiguration(new WorkshopPromotionConfiguration());
            builder.ApplyConfiguration(new RefreshTokenConfiguration());
            builder.ApplyConfiguration(new PasswordResetTokenConfiguration());
            builder.ApplyConfiguration(new BookingConfiguration());
            builder.ApplyConfiguration(new ActivityPointRuleConfiguration());
        }
    }
}
