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
    }
}
