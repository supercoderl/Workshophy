using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Application.ViewModels.Reviews;
using WorkshopHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WorkshopHub.Application.Extensions;
using WorkshopHub.Application.ViewModels.Workshops;
using WorkshopHub.Domain.Interfaces;

namespace WorkshopHub.Application.Queries.Reviews.GetAll
{
    public sealed class GetAllReviewsQueryHandler :
            IRequestHandler<GetAllReviewsQuery, PagedResult<ReviewViewModel>>
    {
        private readonly ISortingExpressionProvider<ReviewViewModel, Review> _sortingExpressionProvider;
        private readonly IUser _user;
        private readonly IReviewRepository _reviewRepository;

        public GetAllReviewsQueryHandler(
            IReviewRepository reviewRepository,
            ISortingExpressionProvider<ReviewViewModel, Review> sortingExpressionProvider,
            IUser user
        )
        {
            _reviewRepository = reviewRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
            _user = user;
        }

        public async Task<PagedResult<ReviewViewModel>> Handle(
            GetAllReviewsQuery request,
            CancellationToken cancellationToken)
        {
            var reviewsQuery = _reviewRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Include(x => x.Workshop)
                .Where(x => request.IncludeDeleted || x.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                reviewsQuery = reviewsQuery.Where(review =>
                    !string.IsNullOrEmpty(review.Comment) && review.Comment.Contains(request.SearchTerm));
            }

            if(request.Filter != null)
            {
                reviewsQuery = FilterReview(request.Filter, reviewsQuery, _user);
            }

            if(request.IsOwner)
            {
                reviewsQuery = reviewsQuery.Where(review => review.Workshop != null && review.Workshop.OrganizerId == _user.GetUserId());
            }

            var totalCount = await reviewsQuery.CountAsync(cancellationToken);

            reviewsQuery = reviewsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            var reviews = await reviewsQuery
                .Skip((request.Query.Page - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize)
                .Select(review => ReviewViewModel.FromReview(review))
                .ToListAsync(cancellationToken);

            return new PagedResult<ReviewViewModel>(
                totalCount, reviews, request.Query.Page, request.Query.PageSize);
        }

        private IQueryable<Review> FilterReview(ReviewFilter filter, IQueryable<Review> reviewQuery, IUser user)
        {
            if (filter.Star.HasValue)
            {
                reviewQuery = reviewQuery.Where(w => w.Rating == filter.Star);
            }

            if (filter.HelpfulCount.HasValue)
            {
      
            }

            if (filter.Date.HasValue)
            {
                reviewQuery = reviewQuery.Where(w => w.CreatedAt == filter.Date.Value);
            }

            if (filter.WorkshopId.HasValue)
            {
                reviewQuery = reviewQuery.Where(w => w.WorkshopId == user.GetUserId());
            }

            return reviewQuery;
        }
    }
}
