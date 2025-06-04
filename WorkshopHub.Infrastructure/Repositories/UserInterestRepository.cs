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
    public sealed class UserInterestRepository : BaseRepository<UserInterest>, IUserInterestRepository
    {
        public UserInterestRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<UserInterest>> GetByCollection(List<Guid> guids, Guid userId)
        {
            return await DbSet.Where(x => x.UserId == userId && guids.Contains(x.CategoryId)).ToListAsync();
        }
    }
}
