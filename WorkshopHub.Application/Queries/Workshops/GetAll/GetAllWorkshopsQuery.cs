using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Workshops;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Application.Queries.Workshops.GetAll
{
    public sealed record GetAllWorkshopsQuery(
        PageQuery Query,
        bool IncludeDeleted,
        string SearchTerm = "",
        WorkshopStatus Status = WorkshopStatus.Approved,
        SortQuery? SortQuery = null,
        WorkshopFilter? Filter = null) :
        IRequest<PagedResult<WorkshopViewModel>>;
}
