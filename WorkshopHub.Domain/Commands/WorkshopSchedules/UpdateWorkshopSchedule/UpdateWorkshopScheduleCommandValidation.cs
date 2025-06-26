using FluentValidation;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.WorkshopSchedules.UpdateWorkshopSchedule
{
    public sealed class UpdateWorkshopScheduleCommandValidation : AbstractValidator<UpdateWorkshopScheduleCommand>
    {
        public UpdateWorkshopScheduleCommandValidation()
        {
            RuleForId();
        }

        public void RuleForId()
        {
            RuleFor(cmd => cmd.Id).NotEmpty().WithErrorCode(DomainErrorCodes.WorkshopSchedule.EmptyId).WithMessage("Id may not be empty");
        }
    }
}
