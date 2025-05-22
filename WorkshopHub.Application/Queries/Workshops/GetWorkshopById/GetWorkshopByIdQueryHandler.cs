using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Queries.Users.GetUserById;
using WorkshopHub.Application.ViewModels.Workshops;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;

namespace WorkshopHub.Application.Queries.Workshops.GetWorkshopById
{
    public sealed class GetWorkshopByIdQueryHandler : IRequestHandler<GetWorkshopByIdQuery, WorkshopViewModel?>
    {
        private readonly IMediatorHandler _bus;
        private readonly IWorkshopRepository _workshopRepository;

        public GetWorkshopByIdQueryHandler(IWorkshopRepository workshopRepository, IMediatorHandler bus)
        {
            _workshopRepository = workshopRepository;
            _bus = bus;
        }

        public async Task<WorkshopViewModel?> Handle(GetWorkshopByIdQuery request, CancellationToken cancellationToken)
        {
            var workshop = await _workshopRepository.GetByIdAsync(request.Id);

            if (workshop is null)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        nameof(GetUserByIdQuery),
                        $"Workshop with id {request.Id} could not be found",
                        ErrorCodes.ObjectNotFound));
                return null;
            }

            return WorkshopViewModel.FromWorkshop(workshop);
        }
    }
}
