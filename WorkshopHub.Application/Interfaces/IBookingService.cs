using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Bookings;

namespace WorkshopHub.Application.Interfaces
{
    public interface IBookingService
    {
        Task<string> CreateBookingAsync(CreateBookingViewModel viewModel);
    }
}
