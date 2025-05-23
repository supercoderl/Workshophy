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

        public void RuleForBuyerName()
        {
            RuleFor(cmd => cmd.BuyerName).NotEmpty().WithErrorCode(DomainErrorCodes.Payment.EmptyBuyerName).WithMessage("Name may not be empty.");
        }

        public void RuleForBuyerEmail()
        {
            RuleFor(cmd => cmd.BuyerEmail)
                .EmailAddress()
                .WithErrorCode(DomainErrorCodes.User.InvalidEmail)
                .WithMessage("Email is invalid")
                .NotEmpty()
                .WithErrorCode(DomainErrorCodes.Payment.EmptyBuyerEmail)
                .WithMessage("Email may not be empty.");
        }

        public void RuleForBuyerPhone()
        {
            RuleFor(cmd => cmd.BuyerPhone).NotEmpty().WithErrorCode(DomainErrorCodes.Payment.EmptyBuyerPhone).WithMessage("Phone may not be empty.");
        }
    }
}
