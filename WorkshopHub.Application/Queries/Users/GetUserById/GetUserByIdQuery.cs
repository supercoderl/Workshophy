using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Users;

namespace WorkshopHub.Application.Queries.Users.GetUserById
{
    public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserViewModel?>;
}
