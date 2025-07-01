using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Bookings;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;

namespace WorkshopHub.Application.Queries.Bookings.GetAllBookingsForCustomer
{
    public sealed class GetAllBookingsForCustomerQueryHandler :
                    IRequestHandler<GetAllBookingsForCustomerQuery, PagedResult<CustomBookingListViewModelForCustomer>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUser _user;

        public GetAllBookingsForCustomerQueryHandler(
            IBookingRepository bookingRepository,
            IUser user
        )
        {
            _bookingRepository = bookingRepository;
            _user = user;
        }

        public async Task<PagedResult<CustomBookingListViewModelForCustomer>> Handle(
            GetAllBookingsForCustomerQuery request,
            CancellationToken cancellationToken)
        {
            var bookingQuery = _bookingRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Include(x => x.Workshop)
                .ThenInclude(w => w.User)
                .Where(x => request.IncludeDeleted || x.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                // Add search term filtering logic here if needed
            }

            bookingQuery = bookingQuery.Where(b => b.UserId == _user.GetUserId());

            var result = await bookingQuery
                .GroupBy(b => b.Workshop != null && b.Workshop.User != null ? b.Workshop.User.AccountBank : null)
                .Select(g => new CustomBookingListViewModelForCustomer
                {
                    AccountBank = g.Key ?? string.Empty,
                    TotalAmount = g.Sum(b => b.TotalPrice),
                    Bookings = g.Select(b => BookingViewModel.FromBooking(b)).ToList()
                })
                .ToListAsync();

            // Assuming totalCount and blogPosts are defined elsewhere in the method
            return new PagedResult<CustomBookingListViewModelForCustomer>(
                result.Count, result, request.Query.Page, request.Query.PageSize);
        }
    }
}
