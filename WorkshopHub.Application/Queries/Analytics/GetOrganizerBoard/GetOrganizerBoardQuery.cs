using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Analytics;

namespace WorkshopHub.Application.Queries.Analytics.GetOrganizerBoard
{
    public sealed record GetOrganizerBoardQuery() : IRequest<OrganizerBoardViewModel>;
}
