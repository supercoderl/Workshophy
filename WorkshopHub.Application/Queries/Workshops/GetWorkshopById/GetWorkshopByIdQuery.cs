using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Workshops;

namespace WorkshopHub.Application.Queries.Workshops.GetWorkshopById
{
    public sealed record GetWorkshopByIdQuery(Guid Id) : IRequest<WorkshopViewModel?>;
}
