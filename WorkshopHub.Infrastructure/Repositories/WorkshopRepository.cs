using Microsoft.EntityFrameworkCore;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Infrastructure.Database;

namespace WorkshopHub.Infrastructure.Repositories
{
    public sealed class WorkshopRepository : BaseRepository<Workshop>, IWorkshopRepository
    {
        public WorkshopRepository(ApplicationDbContext context) : base(context)
        {

        }

        public IQueryable<Workshop> GetByCategories(ICollection<Guid> categoryIds)
        {
            return DbSet.Where(x => categoryIds.Contains(x.CategoryId) && x.Status == Domain.Enums.WorkshopStatus.Approved);
        }

        public async Task<IEnumerable<Guid>> GetIdsByUserId(Guid userId)
        {
            return await DbSet.Where(w => w.OrganizerId == userId).Select(x => x.Id).ToListAsync();
        }
    }
}
