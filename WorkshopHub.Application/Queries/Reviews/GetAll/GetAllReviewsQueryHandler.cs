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

namespace WorkshopHub.Application.Queries.Reviews.GetAll
{
    public sealed class GetAllReviewsQueryHandler :
            IRequestHandler<GetAllReviewsQuery, PagedResult<ReviewViewModel>>
    {
        private readonly ISortingExpressionProvider<ReviewViewModel, Review> _sortingExpressionProvider;
        private readonly IReviewRepository _reviewRepository;

        public GetAllReviewsQueryHandler(
            IReviewRepository reviewRepository,
            ISortingExpressionProvider<ReviewViewModel, Review> sortingExpressionProvider)
        {
            _reviewRepository = reviewRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PagedResult<ReviewViewModel>> Handle(
            GetAllReviewsQuery request,
            CancellationToken cancellationToken)
        {
            var reviewsQuery = _reviewRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(x => request.IncludeDeleted || x.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                reviewsQuery = reviewsQuery.Where(review =>
                    !string.IsNullOrEmpty(review.Comment) && review.Comment.Contains(request.SearchTerm));
            }

            if(request.Filter != null)
            {
                reviewsQuery = FilterReview(request.Filter, reviewsQuery);
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

        private IQueryable<Review> FilterReview(ReviewFilter filter, IQueryable<Review> reviewQuery)
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

            return reviewQuery;
        }
    }
}
