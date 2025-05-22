using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Reviews;

namespace WorkshopHub.Application.Interfaces
{
    public interface IReviewService
    {
        public Task<ReviewViewModel?> GetReviewByIdAsync(Guid reviewId);

        public Task<PagedResult<ReviewViewModel>> GetAllReviewsAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            ReviewFilter? filter = null,
            SortQuery? sortQuery = null
        );

        public Task<Guid> CreateReviewAsync(CreateReviewViewModel review);
        public Task UpdateReviewAsync(UpdateReviewViewModel review);
        public Task DeleteReviewAsync(DeleteReviewViewModel review);
    }
}
