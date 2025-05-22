using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Domain.Commands.Bookings.CreateBooking
{
    public sealed class CreateBookingCommand : CommandBase, IRequest<string>
    {
        private static readonly CreateBookingCommandValidation s_validation = new();

        public Guid BookingId { get; }
        public Guid UserId { get; }
        public Guid WorkshopId { get; }
        public int Quantity { get; }

        public CreateBookingCommand(
            Guid bookingId,
            Guid userId,
            Guid workshopId,
            int quantity
        ) : base(Guid.NewGuid())
        {
            BookingId = bookingId;
            UserId = userId;
            WorkshopId = workshopId;
            Quantity = quantity;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
