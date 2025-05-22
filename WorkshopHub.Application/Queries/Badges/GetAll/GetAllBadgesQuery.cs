using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Badges;

namespace WorkshopHub.Application.Queries.Badges.GetAll
{
    public sealed record GetAllBadgesQuery
    (
        PageQuery Query,
        bool IncludeDeleted,
        string SearchTerm = "",
        SortQuery? SortQuery = null) :
        IRequest<PagedResult<BadgeViewModel>>;
}
