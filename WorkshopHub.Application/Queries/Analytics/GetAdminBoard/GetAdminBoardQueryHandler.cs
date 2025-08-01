using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkshopHub.Application.ViewModels.Analytics;
using WorkshopHub.Domain.Helpers;
using WorkshopHub.Domain.Interfaces.Repositories;

namespace WorkshopHub.Application.Queries.Analytics.GetAdminBoard
{
    public sealed class GetAdminBoardQueryHandler : IRequestHandler<GetAdminBoardQuery, AdminBoardViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;

        public GetAdminBoardQueryHandler(
            IUserRepository userRepository,
            IBookingRepository bookingRepository
        )
        {
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<AdminBoardViewModel> Handle(GetAdminBoardQuery request, CancellationToken cancellationToken)
        {
            var usersQuery = _userRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(x => x.DeletedAt == null);

            var bookingsQuery = _bookingRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(x => x.DeletedAt == null);

            var totalUser = await usersQuery.CountAsync();
            var totalActiveUser = await usersQuery.Where(user => user.Status == Domain.Enums.UserStatus.Active).CountAsync();

            int filterMonth = string.IsNullOrEmpty(request.month)
                ? TimeHelper.GetTimeNow().Month
                : int.Parse(request.month.Split('-')[0]);

            int filterYear = string.IsNullOrEmpty(request.month)
                ? TimeHelper.GetTimeNow().Year
                : int.Parse(request.month.Split('-')[1]);


            var revenueByMonths = await bookingsQuery
                .Where(b => 
                    b.PurchasedAt.HasValue && 
                    b.PurchasedAt.Value.Month == filterMonth &&
                    b.PurchasedAt.Value.Year == filterYear &&
                    b.Status == Domain.Enums.BookingStatus.Paid
                )
                .Select(b => b.TotalPrice).ToListAsync();

            return AdminBoardViewModel.FromAdminBoard(totalUser, totalActiveUser, revenueByMonths);
        }
    }
}
