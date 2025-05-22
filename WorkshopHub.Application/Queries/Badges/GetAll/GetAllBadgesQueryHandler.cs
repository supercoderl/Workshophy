using MediatR;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Application.ViewModels.Badges;
using WorkshopHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WorkshopHub.Application.Extensions;

namespace WorkshopHub.Application.Queries.Badges.GetAll
{
    public sealed class GetAllBadgesQueryHandler :
                IRequestHandler<GetAllBadgesQuery, PagedResult<BadgeViewModel>>
    {
        private readonly ISortingExpressionProvider<BadgeViewModel, Badge> _sortingExpressionProvider;
        private readonly IBadgeRepository _badgeRepository;

        public GetAllBadgesQueryHandler(
            IBadgeRepository badgeRepository,
            ISortingExpressionProvider<BadgeViewModel, Badge> sortingExpressionProvider)
        {
            _badgeRepository = badgeRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PagedResult<BadgeViewModel>> Handle(
            GetAllBadgesQuery request,
            CancellationToken cancellationToken)
        {
            var badgesQuery = _badgeRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(x => request.IncludeDeleted || x.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {

            }

            var totalCount = await badgesQuery.CountAsync(cancellationToken);

            badgesQuery = badgesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            var badges = await badgesQuery
                .Skip((request.Query.Page - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize)
                .Select(badge => BadgeViewModel.FromBadge(badge))
                .ToListAsync(cancellationToken);

            return new PagedResult<BadgeViewModel>(
                totalCount, badges, request.Query.Page, request.Query.PageSize);
        }
    }
}
