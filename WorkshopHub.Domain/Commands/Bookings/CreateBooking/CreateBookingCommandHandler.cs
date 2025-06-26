using MediatR;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Commands.Payments.PayOS.CreateOrder;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Helpers;
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
        private readonly IUser _user;

        public CreateBookingCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IBookingRepository bookingRepository,
            IWorkshopRepository workshopRepository,
            IUserRepository userRepository,
            IUser user
        ) : base(bus, unitOfWork, notifications)
        {
            _bookingRepository = bookingRepository;
            _workshopRepository = workshopRepository;
            _userRepository = userRepository;
            _user = user;
        }

        public async Task<string> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return string.Empty;

            var user = await _userRepository.GetByIdAsync(_user.GetUserId());

            if(user == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any user with id: {_user.GetUserId()}.",
                    ErrorCodes.ObjectNotFound
                ));
                return string.Empty;
            }

            long orderCode = long.Parse(TimeHelper.GetTimeNow().ToString("yyMdHms"));

            var booking = new Entities.Booking(
                request.BookingId,
                _user.GetUserId(),
                request.WorkshopId,
                orderCode,
                request.Quantity,
                request.Price * request.Quantity
            );

            _bookingRepository.Add(booking);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new BookingCreatedEvent(booking.Id));

                return await Bus.QueryAsync(new CreatePayOSOrderCommand(
                    orderCode,
                    booking.TotalPrice,
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
