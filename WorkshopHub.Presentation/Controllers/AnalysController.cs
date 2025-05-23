using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.ViewModels.Analytics;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Presentation.Models;

namespace WorkshopHub.Presentation.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class AnalysController : ApiController
    {
        private readonly IAnalysService _analysService;

        public AnalysController(
            INotificationHandler<DomainNotification> notifications,
            IAnalysService analysService
        ) : base(notifications)
        {
            _analysService = analysService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        [SwaggerOperation("Get analytic for admin")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<AdminBoardViewModel>))]
        public async Task<IActionResult> GetAdminBoardAsync()
        {
            var adminBoard = await _analysService.GetAdminBoardAsync();
            return Response(adminBoard);
        }

        [Authorize(Roles = "Organizer")]
        [HttpGet("organizer")]
        [SwaggerOperation("Get analytic for organizer")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<OrganizerBoardViewModel>))]
        public async Task<IActionResult> GetOrganizerBoardAsync()
        {
            var organizerBoard = await _analysService.GetOrganizerBoardAsync();
            return Response(organizerBoard);
        }
    }
}
