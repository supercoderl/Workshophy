using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels.Bookings
{
    public sealed record CreateBookingViewModel
    (
        Guid WorkshopId,
        int Quantity,
        decimal Price
    );
}
