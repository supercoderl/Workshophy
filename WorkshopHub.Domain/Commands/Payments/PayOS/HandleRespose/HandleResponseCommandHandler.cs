using MediatR;
using Microsoft.Extensions.Logging;
using Net.payOS;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Commands.Mail.SendMail;
using WorkshopHub.Domain.Commands.Users.AddPoint;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Helpers;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Models;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.Ticket;

namespace WorkshopHub.Domain.Commands.Payments.PayOS.HandleRespose
{
    public sealed class HandleResponseCommandHandler : CommandHandlerBase, IRequestHandler<HandleResponseCommand>
    {
        private readonly Net.payOS.PayOS _payOS;
        private readonly ILogger<HandleResponseCommandHandler> _logger;
        private readonly IBookingRepository _bookingRepository;
        private readonly ITicketRepository _ticketRepository;

        public HandleResponseCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            Net.payOS.PayOS payOS,
            ILogger<HandleResponseCommandHandler> logger,
            IBookingRepository bookingRepository,
            ITicketRepository ticketRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _payOS = payOS;
            _logger = logger;
            _bookingRepository = bookingRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task Handle(HandleResponseCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            WebhookData data = _payOS.verifyPaymentWebhookData(request.Body);

            _logger.LogInformation($"Payment response data: {data}");

            var booking = await _bookingRepository.GetByOrderCode(data.orderCode);

            if (booking == null)
            {
                await NotifyAsync(new DomainNotification(
                       request.MessageType,
                       $"There is no any booking with code: {data.orderCode}.",
                       ErrorCodes.ObjectNotFound
                   ));
                return;
            }

            if (data.code != "00")
            {
                booking.SetStatus(Enums.BookingStatus.Failed);
                _bookingRepository.Update(booking);
                await CommitAsync();

                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"An error occured when verifying response data: {data}",
                    ErrorCodes.InvalidObject
                ));
                return;
            }

            if (booking.Status != Enums.BookingStatus.Pending) return;

            booking.SetStatus(Enums.BookingStatus.Paid);
            _bookingRepository.Update(booking);

            var ticket = new Entities.Ticket(
                Guid.NewGuid(),
                booking.UserId,
                booking.WorkshopId
            );

            _ticketRepository.Add(ticket);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new TicketCreatedEvent(ticket.Id));

                if(booking.User != null && booking.Workshop != null && booking.Workshop.User != null)
                {
                    // Send mail here
                    await Bus.SendCommandAsync(new SendMailCommand(
                        booking.User.Email,
                        $"🎟️ Your Ticket is Ready – Payment Confirmed",
                        request.HtmlBody,
                        true,
                        new Dictionary<string, string> {
                            { "UserName", booking.User.FullName },
                            { "EventName", booking.Workshop.Title },
                            { "EventDateTime", "14/09/2025 14:00PM" },
                            { "VenueName", booking.Workshop.Location },
                            { "VenueAddress", "Test" },
                            { "TicketType", "Student" },
                            { "TicketQuantity", booking.Quantity.ToString() },
                            { "TotalAmount", (booking.Workshop.Price * booking.Quantity).ToString() },
                            { "ConfirmationNumber", booking.OrderCode.ToString() },
                            { "TicketId", ticket.Id.ToString() },
                            { "OrganizationName", booking.Workshop.User.FullName },
                            { "SupportEmail", booking.Workshop.User.Email },
                            { "SupportPhone", booking.Workshop.User.PhoneNumber },
                            { "CurrentYear", TimeHelper.GetTimeNow().Year.ToString() },
                            { "WebsiteUrl", "https://workshophy.com" },
                            { "FacebookUrl", "https://facebook.com/workshophy" },
                            { "TwitterUrl", "https://x.com/workshophy" },
                            { "InstagramUrl", "https://instagram.com/workshophy" },
                            { "UnsubscribeUrl", "https://buymeacoffee.com/workshophy" },
                            { "QRCodeImage", "https://docs.lightburnsoftware.com/legacy/img/QRCode/ExampleCode.png" }
                        },
                        new List<EmailAttachment>(),
                        new Dictionary<string, string> { }
                    ));

                    // Add point for user
                    await Bus.SendCommandAsync(new AddPointCommand(booking.UserId, "AdtendWorkshop"));
                }
            }
        }
    }
}
