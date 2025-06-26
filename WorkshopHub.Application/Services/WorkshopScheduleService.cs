using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.ViewModels.WorkshopSchedules;
using WorkshopHub.Domain.Commands.WorkshopSchedules.CreateWorkshopSchedule;
using WorkshopHub.Domain.Commands.WorkshopSchedules.UpdateWorkshopSchedule;
using WorkshopHub.Domain.Interfaces;

namespace WorkshopHub.Application.Services
{
    public class WorkshopScheduleService : IWorkshopScheduleService
    {
        private readonly IMediatorHandler _bus;

        public WorkshopScheduleService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreateWorkshopScheduleAsync(CreateWorkshopScheduleViewModel viewModel)
        {
            var id = Guid.NewGuid();
            await _bus.SendCommandAsync(new CreateWorkshopScheduleCommand(
                id,
                viewModel.WorkshopId,
                viewModel.StartTime,
                viewModel.EndTime
            ));
            return id;
        }

        public async Task UpdateWorkshopScheduleAsync(UpdateWorkshopScheduleViewModel viewModel)
        {
            await _bus.SendCommandAsync(new UpdateWorkshopScheduleCommand(
                viewModel.WorkshopScheduleId,
                viewModel.StartTime,
                viewModel.EndTime,
                viewModel.ScheduleStatus
            ));
        }
    }
}
