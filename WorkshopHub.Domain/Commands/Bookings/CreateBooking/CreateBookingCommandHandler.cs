using MediatR;
using Net.payOS.Types;
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
        private readonly IUserRepository _userRepository;

        public CreateBookingCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IBookingRepository bookingRepository,
            IWorkshopRepository workshopRepository,
            IUserRepository userRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _bookingRepository = bookingRepository;
            _workshopRepository = workshopRepository;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return string.Empty;

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if(user == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any user with id: {request.UserId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return string.Empty;
            }

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
                    $" Pay for workshop.",
                    new List<ItemData>(),
                    user.FullName,
                    user.Email,
                    "",
                    ""
                ));
            }

            return string.Empty;
        }
    }
}
