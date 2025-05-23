using MediatR;
using Net.payOS.Types;
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
        public List<ItemData> Items { get; }
        public string BuyerName { get; }
        public string BuyerEmail { get; }
        public string BuyerPhone { get; }
        public string? BuyerAddress { get; }

        public CreatePayOSOrderCommand(
            decimal price,
            string description,
            List<ItemData> items,
            string buyerName,
            string buyerEmail,
            string buyerPhone,
            string? buyerAddress
        ) : base(Guid.NewGuid())
        {
            Price = price;
            Description = description;
            Items = items;
            BuyerName = buyerName;
            BuyerEmail = buyerEmail;
            BuyerPhone = buyerPhone;
            BuyerAddress = buyerAddress;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
