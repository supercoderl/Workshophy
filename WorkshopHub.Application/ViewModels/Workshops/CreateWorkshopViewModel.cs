using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels.Workshops
{
    public sealed record CreateWorkshopViewModel
    (
        Guid OrganizerId,
        string Title,
        string? Description,
        Guid CategoryId,
        string Location,
        string? IntroVideoUrl,
        decimal Price,
        DateTime StartTime,
        DateTime EndTime
    );
}
