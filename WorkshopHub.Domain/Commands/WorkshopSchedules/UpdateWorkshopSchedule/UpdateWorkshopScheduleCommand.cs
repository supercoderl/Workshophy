using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Domain.Commands.WorkshopSchedules.UpdateWorkshopSchedule
{
    public sealed class UpdateWorkshopScheduleCommand : CommandBase, IRequest
    {
        private static readonly UpdateWorkshopScheduleCommandValidation s_validation = new();

        public Guid Id { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public ScheduleStatus ScheduleStatus { get; }

        public UpdateWorkshopScheduleCommand(
            Guid id,
            DateTime startTime,
            DateTime endTime,
            ScheduleStatus scheduleStatus
        ) : base(Guid.NewGuid())
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            ScheduleStatus = scheduleStatus;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
