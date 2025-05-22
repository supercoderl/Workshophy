using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels.Workshops;
using WorkshopHub.Domain.Commands.Workshops.CreateWorkshop;
using WorkshopHub.Domain.Commands.Workshops.DeleteWorkshop;
using WorkshopHub.Domain.Commands.Workshops.UpdateWorkshop;
using WorkshopHub.Domain.Enums;
using WorkshopHub.Domain.Interfaces;

namespace WorkshopHub.Application.Services
{
    public class WorkshopService : IWorkshopService
    {
        private readonly IMediatorHandler _bus;

        public WorkshopService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreateWorkshopAsync(CreateWorkshopViewModel workshop)
        {
            var workshopId = Guid.NewGuid();
            await _bus.SendCommandAsync(new CreateWorkshopCommand(
                workshopId,
                workshop.OrganizerId,
                workshop.Title,
                workshop.Description,
                workshop.CategoryId,
                workshop.Location,
                workshop.IntroVideoUrl,
                workshop.DurationMinutes,
                workshop.Price
            ));

            return workshopId;
        }

        public async Task DeleteWorkshopAsync(DeleteWorkshopViewModel workshop)
        {
            await _bus.SendCommandAsync(new DeleteWorkshopCommand(workshop.WorkshopId));
        }

        public Task<PagedResult<WorkshopViewModel>> GetAllWorkshopsAsync(PageQuery query, bool includeDeleted, string searchTerm = "", WorkshopStatus status = WorkshopStatus.Approved, SortQuery? sortQuery = null, WorkshopFilter? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<WorkshopViewModel?> GetWorkshopByIdAsync(Guid workshopId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateWorkshopAsync(UpdateWorkshopViewModel workshop)
        {
            await _bus.SendCommandAsync(new UpdateWorkshopCommand(
                workshop.WorkshopId,
                workshop.OrganizerId,
                workshop.Title,
                workshop.Description,
                workshop.CategoryId,
                workshop.Location,
                workshop.IntroVideoUrl,
                workshop.DurationMinutes,
                workshop.Price,
                workshop.Status
            ));
        }
    }
}
