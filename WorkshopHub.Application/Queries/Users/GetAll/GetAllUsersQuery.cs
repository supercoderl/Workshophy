using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels.Users;

namespace WorkshopHub.Application.Queries.Users.GetAll
{
    public sealed record GetAllUsersQuery(
        PageQuery Query,
        bool IncludeDeleted,
        string SearchTerm = "",
        SortQuery? SortQuery = null) :
        IRequest<PagedResult<UserViewModel>>;
}
