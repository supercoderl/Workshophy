using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.Queries.Reviews.GetAll;
using WorkshopHub.Application.Queries.Reviews.GetReviewById;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Reviews;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Domain.Commands.Reviews.CreateReview;
using WorkshopHub.Domain.Commands.Reviews.DeleteReview;
using WorkshopHub.Domain.Commands.Reviews.UpdateReview;
using WorkshopHub.Domain.Interfaces;

namespace WorkshopHub.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IMediatorHandler _bus;

        public ReviewService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreateReviewAsync(CreateReviewViewModel review)
        {
            var reviewId = Guid.NewGuid();
            await _bus.SendCommandAsync(new CreateReviewCommand(
                reviewId,
                review.UserId,
                review.WorkshopId,
                review.Rating,
                review.Comment
            ));

            return reviewId;
        }

        public async Task DeleteReviewAsync(DeleteReviewViewModel review)
        {
            await _bus.SendCommandAsync(new DeleteReviewCommand(review.ReviewId));
        }

        public async Task<PagedResult<ReviewViewModel>> GetAllReviewsAsync(
            PageQuery query, 
            bool includeDeleted, 
            string searchTerm = "", 
            ReviewFilter? filter = null,
            SortQuery? sortQuery = null,
            bool isOwner = false
        )
        {
            return await _bus.QueryAsync(new GetAllReviewsQuery(query, includeDeleted, searchTerm, filter, sortQuery, isOwner));
        }

        public async Task<ReviewViewModel?> GetReviewByIdAsync(Guid reviewId)
        {
            return await _bus.QueryAsync(new GetReviewByIdQuery(reviewId));
        }

        public async Task UpdateReviewAsync(UpdateReviewViewModel review)
        {
            await _bus.SendCommandAsync(new UpdateReviewCommand(
                review.ReviewId,
                review.UserId,
                review.WorkshopId,
                review.Rating,
                review.Comment
            ));
        }
    }
}
