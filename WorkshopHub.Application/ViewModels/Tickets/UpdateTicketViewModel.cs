using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels.Tickets
{
    public sealed record UpdateTicketViewModel
    (
        Guid TicketId,
        Guid UserId,
        Guid WorkshopId
    );
}
