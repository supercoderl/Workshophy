using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.SortProviders;
using WorkshopHub.Application.ViewModels.BlogPosts;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Presentation.Models;
using WorkshopHub.Presentation.Swagger;
using WorkshopHub.Application.ViewModels.Tickets;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/v1/[controller]")]
    public sealed class TicketController : ApiController
    {
        private readonly ITicketService _ticketService;

        public TicketController(
            INotificationHandler<DomainNotification> notifications,
            ITicketService ticketService
        ) : base(notifications)
        {
            _ticketService = ticketService;
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [SwaggerOperation("Get a list of all tickets for customer")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<TicketViewModel>>))]
        public async Task<IActionResult> GetAllTicketsForUserAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<TicketViewModelSortProvider, TicketViewModel, Ticket>]
            SortQuery? sortQuery = null
        )
        {
            var tickets = await _ticketService.GetAllTicketsAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery
            );
            return Response(tickets);
        }
    }
}
