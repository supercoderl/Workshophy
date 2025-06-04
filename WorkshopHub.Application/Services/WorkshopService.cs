using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.Queries.Workshops.GetAll;
using WorkshopHub.Application.Queries.Workshops.GetWorkshopById;
using WorkshopHub.Application.Queries.Workshops.GetWorkshopsByCategories;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels.Workshops;
using WorkshopHub.Domain.Commands.Workshops.ApproveWorkshop;
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

        public async Task ApproveWorkshopAsync(Guid id, ApproveWorkshopViewModel workshop)
        {
            await _bus.SendCommandAsync(new ApproveWorkshopCommand(id, workshop.IsAccept));
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

        public async Task<PagedResult<WorkshopViewModel>> GetAllWorkshopsAsync(PageQuery query, bool includeDeleted, string searchTerm = "", WorkshopStatus status = WorkshopStatus.Approved, SortQuery? sortQuery = null, WorkshopFilter? filter = null, bool isOwner = false)
        {
            return await _bus.QueryAsync(new GetAllWorkshopsQuery(query, includeDeleted, searchTerm, status, sortQuery, filter, isOwner));
        }

        public async Task<PagedResult<WorkshopViewModel>> GetRecommendWorkshopsAsync(PageQuery query)
        {
            return await _bus.QueryAsync(new GetWorkshopsByCategoriesQuery(query));
        }

        public async Task<WorkshopViewModel?> GetWorkshopByIdAsync(Guid workshopId)
        {
            return await _bus.QueryAsync(new GetWorkshopByIdQuery(workshopId));
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
