using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.Queries.Bookings.GetAllBookingsForCustomer;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Bookings;
using WorkshopHub.Domain.Commands.Bookings.CreateBooking;
using WorkshopHub.Domain.Interfaces;

namespace WorkshopHub.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IMediatorHandler _bus;

        public BookingService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<string> CreateBookingAsync(CreateBookingViewModel viewModel)
        {
            return await _bus.QueryAsync(new CreateBookingCommand(
                Guid.NewGuid(),
                viewModel.WorkshopId,
                viewModel.Quantity,
                viewModel.Price
            ));
        }

        public async Task<PagedResult<CustomBookingListViewModelForCustomer>> GetAllBookingsForCustomerAsync(PageQuery query, bool includeDeleted, string searchTerm = "")
        {
            return await _bus.QueryAsync(new GetAllBookingsForCustomerQuery(query, includeDeleted, searchTerm));
        }
    }
}
