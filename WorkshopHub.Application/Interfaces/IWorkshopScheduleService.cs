using WorkshopHub.Application.ViewModels.WorkshopSchedules;

namespace WorkshopHub.Application.Interfaces
{
    public interface IWorkshopScheduleService
    {
        public Task<Guid> CreateWorkshopScheduleAsync(CreateWorkshopScheduleViewModel viewModel);
        public Task UpdateWorkshopScheduleAsync(UpdateWorkshopScheduleViewModel viewModel);
    }
}
