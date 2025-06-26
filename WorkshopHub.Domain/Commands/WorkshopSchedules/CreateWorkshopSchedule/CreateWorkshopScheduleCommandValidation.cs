using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.WorkshopSchedules.CreateWorkshopSchedule
{
    public sealed class CreateWorkshopScheduleCommandValidation : AbstractValidator<CreateWorkshopScheduleCommand>
    {
        public CreateWorkshopScheduleCommandValidation()
        {
            RuleForWorkshopId();
        }

        public void RuleForWorkshopId()
        {
            RuleFor(cmd => cmd.WorkshopId).NotEmpty().WithErrorCode(DomainErrorCodes.WorkshopSchedule.EmptyWorkshopId).WithMessage("Workshop id may not be empty");
        }
    }
}
