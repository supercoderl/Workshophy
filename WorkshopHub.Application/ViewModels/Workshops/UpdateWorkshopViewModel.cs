using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Application.ViewModels.Workshops
{
    public sealed record UpdateWorkshopViewModel
    (
        Guid WorkshopId,
        Guid OrganizerId,
        string Title,
        string? Description,
        Guid CategoryId,
        string Location,
        string? IntroVideoUrl,
        decimal Price,
        WorkshopStatus Status,
        DateTime StartTime,
        DateTime EndTime,
        ScheduleStatus ScheduleStatus
    );
}
