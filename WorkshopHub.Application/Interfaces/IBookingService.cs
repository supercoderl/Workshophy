using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Bookings;

namespace WorkshopHub.Application.Interfaces
{
    public interface IBookingService
    {
        public Task<PagedResult<CustomBookingListViewModelForCustomer>> GetAllBookingsForCustomerAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = ""
        );
        public Task<BookingGroupByOwnerViewModel> GetAllBookingsForAnalysAsync(Guid userId);
        Task<string> CreateBookingAsync(CreateBookingViewModel viewModel);
    }
}
