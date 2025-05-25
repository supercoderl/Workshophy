using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Infrastructure.Configurations
{
    public sealed class ActivityPointRuleConfiguration : IEntityTypeConfiguration<ActivityPointRule>
    {
        public void Configure(EntityTypeBuilder<ActivityPointRule> builder)
        {
            builder
                .Property(a => a.ActivityType)
                .IsRequired();

            builder
                .Property(a => a.ActivityPoint)
                .IsRequired();

            builder.HasData(new ActivityPointRule(
                Guid.Parse("9c052706-7218-4d52-a5ad-4e7071eba219"),
                "AdtendWorkshop",
                10
            ));
        }
    }
}
