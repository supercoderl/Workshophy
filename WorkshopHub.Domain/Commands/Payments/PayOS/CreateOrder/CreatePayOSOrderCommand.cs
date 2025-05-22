using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Payments.PayOS.CreateOrder
{
    public sealed class CreatePayOSOrderCommand : CommandBase, IRequest<string>
    {
        private static readonly CreatePayOSOrderCommandValidation s_validation = new();

        public decimal Price { get; }
        public string Description { get; }

        public CreatePayOSOrderCommand(
            decimal price,
            string description
        ) : base(Guid.NewGuid())
        {
            Price = price;
            Description = description;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
