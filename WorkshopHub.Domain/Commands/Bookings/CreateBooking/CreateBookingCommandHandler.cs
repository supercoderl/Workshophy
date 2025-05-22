using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Commands.Payments.PayOS.CreateOrder;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.Booking;

namespace WorkshopHub.Domain.Commands.Bookings.CreateBooking
{
    public sealed class CreateBookingCommandHandler : CommandHandlerBase, IRequestHandler<CreateBookingCommand, string>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IWorkshopRepository _workshopRepository;

        public CreateBookingCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IBookingRepository bookingRepository,
            IWorkshopRepository workshopRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _bookingRepository = bookingRepository;
            _workshopRepository = workshopRepository;
        }

        public async Task<string> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return string.Empty;

            var booking = new Entities.Booking(
                request.BookingId,
                request.UserId,
                request.WorkshopId,
                request.Quantity
            );

            _bookingRepository.Add(booking);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new BookingCreatedEvent(booking.Id));
                var workshop = await _workshopRepository.GetByIdAsync(booking.WorkshopId);

                if(workshop == null)
                {
                    await NotifyAsync(new DomainNotification(
                        request.MessageType,
                        $"There is no any workshop with id: {booking.WorkshopId}.",
                        ErrorCodes.ObjectNotFound
                    ));
                    return string.Empty;
                }

                return await Bus.QueryAsync(new CreatePayOSOrderCommand(
                    workshop.Price * booking.Quantity,
                    $"Pay for the order for the workshop: {booking.WorkshopId}"
                ));
            }

            return string.Empty;
        }
    }
}
