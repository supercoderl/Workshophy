using Aikido.Zen.Core;
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
    public sealed class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<Workshop?> GetBestRatingWorkshop(Guid userId)
        {
            return  await DbSet
                .Where(r => r.Workshop != null && r.Workshop.OrganizerId == userId)
                .GroupBy(r => r.Workshop)
                .Select(g => new {
                    Workshop = g.Key,
                    AverageRating = g.Average(r => r.Rating)
                })
                .OrderByDescending(g => g.AverageRating)
                .Select(g => g.Workshop)
                .FirstOrDefaultAsync();
        }

        public async Task<Review?> GetCurrentReviewByUser(Guid userId)
        {
            return await DbSet
                .Include(r => r.Workshop)
                .Where(r => r.Workshop != null && r.Workshop.OrganizerId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .SingleOrDefaultAsync();
        }
    }
}
