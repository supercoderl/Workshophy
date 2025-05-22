using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Payments.PayOS.CreateOrder
{
    public sealed class CreatePayOSOrderCommandValidation : AbstractValidator<CreatePayOSOrderCommand>
    {
        public CreatePayOSOrderCommandValidation()
        {
            RuleForDescription();
        }

        public void RuleForDescription()
        {
            RuleFor(cmd => cmd.Description).NotEmpty().WithErrorCode(DomainErrorCodes.Payment.EmptyDescription).WithMessage("Description may not be empty.");
        }
    }
}
