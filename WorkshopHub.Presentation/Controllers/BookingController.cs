using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.ViewModels.Bookings;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Presentation.Models;

namespace WorkshopHub.Presentation.Controllers
{

    [ApiController]
    [Route("/api/v1/[controller]")]
    public class BookingController : ApiController
    {
        private readonly IBookingService _bookingService;

        public BookingController(
            INotificationHandler<DomainNotification> notifications,
            IBookingService bookingService
        ) : base(notifications)
        {
            _bookingService = bookingService;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [SwaggerOperation("Create a booking")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<string>))]
        public async Task<IActionResult> CreateBookingAsync([FromBody] CreateBookingViewModel viewModel)
        {
            var paymentUrl = await _bookingService.CreateBookingAsync(viewModel);
            return Response(paymentUrl);
        }
    }
}
