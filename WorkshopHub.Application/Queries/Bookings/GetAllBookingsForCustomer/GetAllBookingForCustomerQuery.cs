using MediatR;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Bookings;

namespace WorkshopHub.Application.Queries.Bookings.GetAllBookingsForCustomer
{
    public sealed record GetAllBookingsForCustomerQuery
    (
        PageQuery Query,
        bool IncludeDeleted,
        string SearchTerm = "") :
        IRequest<PagedResult<CustomBookingListViewModelForCustomer>>;
}
