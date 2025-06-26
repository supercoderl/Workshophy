using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.WorkshopSchedules.CreateWorkshopSchedule
{
    public sealed class CreateWorkshopScheduleCommand : CommandBase, IRequest
    {
        private static readonly CreateWorkshopScheduleCommandValidation s_validation = new();

        public Guid Id { get; }
        public Guid WorkshopId { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public CreateWorkshopScheduleCommand(
            Guid id,
            Guid workshopId,
            DateTime startTime,
            DateTime endTime
        ) : base(Guid.NewGuid())
        {
            Id = id;
            WorkshopId = workshopId;
            StartTime = startTime;
            EndTime = endTime;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
