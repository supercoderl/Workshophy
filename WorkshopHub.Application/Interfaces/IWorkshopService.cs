using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Workshops;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Application.Interfaces
{
    public interface IWorkshopService
    {
        public Task<WorkshopViewModel?> GetWorkshopByIdAsync(Guid workshopId);

        public Task<PagedResult<WorkshopViewModel>> GetAllWorkshopsAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            WorkshopStatus status = WorkshopStatus.Approved,
            SortQuery? sortQuery = null,
            WorkshopFilter? filter = null,
            bool isOwner = false
        );

        public Task<Guid> CreateWorkshopAsync(CreateWorkshopViewModel workshop);
        public Task UpdateWorkshopAsync(UpdateWorkshopViewModel workshop);
        public Task DeleteWorkshopAsync(DeleteWorkshopViewModel workshop);
        public Task ApproveWorkshopAsync(Guid id, ApproveWorkshopViewModel workshop);
    }
}
