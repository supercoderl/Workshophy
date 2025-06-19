using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Bookings.CreateBooking
{
    public sealed class CreateBookingCommandValidation : AbstractValidator<CreateBookingCommand>    
    {
        public CreateBookingCommandValidation()
        {
            RuleForWorkshopId();
        }

        public void RuleForWorkshopId()
        {
            RuleFor(cmd => cmd.WorkshopId).NotEmpty().WithErrorCode(DomainErrorCodes.Booking.EmptyWorkshopId).WithMessage("Workshop id may not be empty.");
        }
    }
}
