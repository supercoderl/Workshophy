using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels.Reviews
{
    public sealed record UpdateReviewViewModel
    (
        Guid ReviewId,
        Guid UserId,
        Guid WorkshopId,
        int Rating,
        string? Comment
    );
}
