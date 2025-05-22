using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Badges;

namespace WorkshopHub.Application.Interfaces
{
    public interface IBadgeService
    {
        public Task<BadgeViewModel?> GetBadgeByIdAsync(Guid badgeId);

        public Task<PagedResult<BadgeViewModel>> GetAllBadgesAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null
        );

        public Task<Guid> CreateBadgeAsync(CreateBadgeViewModel badge);
        public Task UpdateBadgeAsync(UpdateBadgeViewModel badge);
        public Task DeleteBadgeAsync(DeleteBadgeViewModel badge);
    }
}
