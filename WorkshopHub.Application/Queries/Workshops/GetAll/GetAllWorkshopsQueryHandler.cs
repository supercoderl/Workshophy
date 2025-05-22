using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkshopHub.Application.Extensions;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels.Workshops;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;

namespace WorkshopHub.Application.Queries.Workshops.GetAll
{
    public sealed class GetAllWorkshopsQueryHandler : IRequestHandler<GetAllWorkshopsQuery, PagedResult<WorkshopViewModel>>
    {
        private readonly IWorkshopRepository _workshopRepository;
        private readonly IMediatorHandler _bus;
        private readonly ISortingExpressionProvider<WorkshopViewModel, Workshop> _sortingExpressionProvider;

        public GetAllWorkshopsQueryHandler(
            IWorkshopRepository workshopRepository, 
            IMediatorHandler bus,
            ISortingExpressionProvider<WorkshopViewModel, Workshop> sortingExpressionProvider
        )
        {
            _workshopRepository = workshopRepository;
            _bus = bus;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PagedResult<WorkshopViewModel>> Handle(GetAllWorkshopsQuery request, CancellationToken cancellationToken)
        {
            var workshopsQuery = _workshopRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(x => request.IncludeDeleted || x.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                workshopsQuery = workshopsQuery.Where(workshop =>
                    workshop.Title.Contains(request.SearchTerm) ||
                    workshop.Location.Contains(request.SearchTerm));
            }

            workshopsQuery = workshopsQuery.Where(w => w.Status == request.Status);

            if (request.Filter != null)
            {
                workshopsQuery = FilterWorkshop(request.Filter, workshopsQuery);
            }

            var totalCount = await workshopsQuery.CountAsync(cancellationToken);

            workshopsQuery = workshopsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            var workshops = await workshopsQuery
                .Skip((request.Query.Page - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize)
                .Select(workshop => WorkshopViewModel.FromWorkshop(workshop))
                .ToListAsync(cancellationToken);

            return new PagedResult<WorkshopViewModel>(
                totalCount, workshops, request.Query.Page, request.Query.PageSize);
        }

        private IQueryable<Workshop> FilterWorkshop(WorkshopFilter filter, IQueryable<Workshop> workshopQuery)
        {
            if(filter.CategoryId.HasValue)
            {
                workshopQuery = workshopQuery.Where(w => w.CategoryId == filter.CategoryId);
            }

            if(!string.IsNullOrEmpty(filter.Location))
            {
                workshopQuery = workshopQuery.Where(w => w.Location == filter.Location);
            }

            if (filter.FromDate.HasValue)
            {
                workshopQuery = workshopQuery.Where(w => w.CreatedAt >= filter.FromDate.Value);
            }

            if (filter.ToDate.HasValue)
            {
                workshopQuery = workshopQuery.Where(w => w.CreatedAt <= filter.ToDate.Value);
            }

            if (filter.MinPrice.HasValue)
            {
                workshopQuery = workshopQuery.Where(w => w.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                workshopQuery = workshopQuery.Where(w => w.Price <= filter.MaxPrice.Value);
            }

            if(filter.IsOnSale)
            {
                
            }

            return workshopQuery;
        }
    }
}
