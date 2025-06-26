using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels.WorkshopSchedules
{
    public sealed record CreateWorkshopScheduleViewModel
    (
        Guid WorkshopId,
        DateTime StartTime,
        DateTime EndTime
    );
}
