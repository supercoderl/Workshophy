using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkshopHub.Application.ViewModels.Bookings;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;

namespace WorkshopHub.Application.Queries.Bookings.GetAllBookingsForAnalys
{
    public sealed class GetAllBookingsForAnalysQueryHandler :
                        IRequestHandler<GetAllBookingsForAnalysQuery, BookingGroupByOwnerViewModel>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IWorkshopRepository _workshopRepository;
        private readonly IUser _user;

        public GetAllBookingsForAnalysQueryHandler(
            IBookingRepository bookingRepository,
            IWorkshopRepository workshopRepository,
            IUser user
        )
        {
            _bookingRepository = bookingRepository;
            _workshopRepository = workshopRepository;
            _user = user;
        }

        public async Task<BookingGroupByOwnerViewModel> Handle(
            GetAllBookingsForAnalysQuery request,
            CancellationToken cancellationToken)
        {
            var workshopIds = await _workshopRepository.GetIdsByUserId(request.UserId);

            var bookingQuery = _bookingRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Include(x => x.Workshop)
                .ThenInclude(w => w.User)
                .Where(x => x.DeletedAt == null);

            bookingQuery = bookingQuery.Where(b => workshopIds.Contains(b.WorkshopId));

            var bookings = await bookingQuery.ToListAsync();

            if (!bookings.Any())
            {
                return new BookingGroupByOwnerViewModel();
            }

            var workshopGroups = bookings
            .GroupBy(b => b.WorkshopId)
            .Select(g =>
            {
                var workshop = g.First().Workshop;
                return new WorkshopBookingGroupViewModel
                {
                    WorkshopId = workshop?.Id ?? Guid.Empty,
                    WorkshopName = workshop?.Title ?? string.Empty,
                    TotalAmount = g.Sum(b => b.TotalPrice),
                    Bookings = g.Select(b => BookingViewModel.FromBooking(b)).ToList()
                };
            })
            .ToList();

            var owner = bookings.First().Workshop?.User;

            var result = new BookingGroupByOwnerViewModel
            {
                OwnerName = owner?.FullName ?? string.Empty,
                AccountBank = owner?.AccountBank ?? string.Empty,
                TotalAmount = workshopGroups.Sum(w => w.TotalAmount),
                Workshops = workshopGroups
            };

            // Assuming totalCount and blogPosts are defined elsewhere in the method
            return result;
        }
    }
}
