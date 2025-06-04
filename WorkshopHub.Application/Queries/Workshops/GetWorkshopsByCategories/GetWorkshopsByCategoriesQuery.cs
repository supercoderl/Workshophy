using MediatR;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Workshops;

namespace WorkshopHub.Application.Queries.Workshops.GetWorkshopsByCategories
{
    public sealed record GetWorkshopsByCategoriesQuery
    (
        PageQuery Query
    ) : IRequest<PagedResult<WorkshopViewModel>>;
}
