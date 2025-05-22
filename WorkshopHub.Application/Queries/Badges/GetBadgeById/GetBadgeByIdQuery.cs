using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Badges;

namespace WorkshopHub.Application.Queries.Badges.GetBadgeById
{
    public sealed record GetBadgeByIdQuery(Guid Id) : IRequest<BadgeViewModel?>;
}
