﻿using MediatR;
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
        public Guid WorkshopId { get; }
        public int Quantity { get; }
        public decimal Price { get; }

        public CreateBookingCommand(
            Guid bookingId,
            Guid workshopId,
            int quantity,
            decimal price
        ) : base(Guid.NewGuid())
        {
            BookingId = bookingId;
            WorkshopId = workshopId;
            Quantity = quantity;
            Price = price;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
