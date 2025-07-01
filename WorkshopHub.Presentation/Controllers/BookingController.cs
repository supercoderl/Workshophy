using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.ViewModels;
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

        [Authorize(Roles = "Customer")]
        [HttpGet("customer")]
        [SwaggerOperation("Get all bookings for customer")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<CustomBookingListViewModelForCustomer>>))]
        public async Task<IActionResult> GetAllBookingsForCustomerAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false
        )
        {
            var bookings = await _bookingService.GetAllBookingsForCustomerAsync(query, includeDeleted, searchTerm);
            return Response(bookings);
        }

        [Authorize(Roles = "Admin, Organizer")]
        [HttpGet("analys")]
        [SwaggerOperation("Get all bookings for analys")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<CustomBookingListViewModelForCustomer>>))]
        public async Task<IActionResult> GetAllBookingsForAnalysAsync(
            [FromQuery] Guid userId
        )
        {
            var bookings = await _bookingService.GetAllBookingsForAnalysAsync(userId);
            return Response(bookings);
        }
    }
}
