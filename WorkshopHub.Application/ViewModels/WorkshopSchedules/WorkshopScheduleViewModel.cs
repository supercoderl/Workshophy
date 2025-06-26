using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Application.ViewModels.WorkshopSchedules
{
    public sealed class WorkshopScheduleViewModel
    {
        public Guid WorkshopScheduleId { get; set; }
        public Guid WorkshopId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ScheduleStatus ScheduleStatus { get; set; }

        public static WorkshopScheduleViewModel FromWorkshopSchedule(WorkshopSchedule workshopSchedule)
        {
            return new WorkshopScheduleViewModel
            {
                WorkshopScheduleId = workshopSchedule.Id,
                WorkshopId = workshopSchedule.WorkshopId,
                StartTime = workshopSchedule.StartTime,
                EndTime = workshopSchedule.EndTime,
                ScheduleStatus = workshopSchedule.ScheduleStatus,
            };
        }
    }
}
