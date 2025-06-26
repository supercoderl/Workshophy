using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Application.ViewModels.WorkshopSchedules
{
    public sealed record UpdateWorkshopScheduleViewModel
    (
        Guid WorkshopScheduleId,
        DateTime StartTime,
        DateTime EndTime,
        ScheduleStatus ScheduleStatus
    );
}
