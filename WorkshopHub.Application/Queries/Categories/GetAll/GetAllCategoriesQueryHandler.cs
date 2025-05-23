using MediatR;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Application.ViewModels.Categories;
using WorkshopHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WorkshopHub.Application.Extensions;

namespace WorkshopHub.Application.Queries.Categories.GetAll
{
    public sealed class GetAllCategoriesQueryHandler :
                IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryViewModel>>
    {
        private readonly ISortingExpressionProvider<CategoryViewModel, Category> _sortingExpressionProvider;
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(
            ICategoryRepository categoryRepository,
            ISortingExpressionProvider<CategoryViewModel, Category> sortingExpressionProvider)
        {
            _categoryRepository = categoryRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PagedResult<CategoryViewModel>> Handle(
            GetAllCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var categoriesQuery = _categoryRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(x => request.IncludeDeleted || x.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {

            }

            var totalCount = await categoriesQuery.CountAsync(cancellationToken);

            categoriesQuery = categoriesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            var categories = await categoriesQuery
                .Skip((request.Query.Page - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize)
                .Select(category => CategoryViewModel.FromCategory(category))
                .ToListAsync(cancellationToken);

            return new PagedResult<CategoryViewModel>(
                totalCount, categories, request.Query.Page, request.Query.PageSize);
        }
    }
}
