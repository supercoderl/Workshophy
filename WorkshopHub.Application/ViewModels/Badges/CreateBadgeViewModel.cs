using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels.Badges
{
    public sealed record CreateBadgeViewModel
    (
        string Name,
        string? Description,
        string Area,
        string ImageUrl
    );
}
