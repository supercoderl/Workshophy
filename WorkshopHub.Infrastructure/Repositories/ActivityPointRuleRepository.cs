using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Infrastructure.Database;

namespace WorkshopHub.Infrastructure.Repositories
{
    public sealed class ActivityPointRuleRepository : BaseRepository<ActivityPointRule>, IActivityPointRuleRepository
    {
        public ActivityPointRuleRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<ActivityPointRule?> GetByType(string type)
        {
            return await DbSet.Where(a => a.ActivityType.Equals(type)).SingleOrDefaultAsync();
        }
    }
}
