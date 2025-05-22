using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Reviews;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;

namespace WorkshopHub.Application.Queries.Reviews.GetAll
{
    public sealed record GetAllReviewsQuery(
        PageQuery Query,
        bool IncludeDeleted,
        string SearchTerm = "",
        ReviewFilter? Filter = null,
        SortQuery? SortQuery = null) :
        IRequest<PagedResult<ReviewViewModel>>;
}
