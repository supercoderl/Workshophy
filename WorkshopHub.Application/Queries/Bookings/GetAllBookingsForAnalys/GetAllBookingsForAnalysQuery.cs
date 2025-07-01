using MediatR;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Bookings;

namespace WorkshopHub.Application.Queries.Bookings.GetAllBookingsForAnalys
{
    public sealed record GetAllBookingsForAnalysQuery
    (
        Guid UserId
    ) :
        IRequest<BookingGroupByOwnerViewModel>;
}
