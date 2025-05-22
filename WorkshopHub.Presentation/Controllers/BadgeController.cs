using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.SortProviders;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Presentation.Models;
using WorkshopHub.Presentation.Swagger;
using WorkshopHub.Application.ViewModels.Badges;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/v1/[controller]")]
    public sealed class BadgeController : ApiController
    {
        private readonly IBadgeService _badgeService;

        public BadgeController(
            INotificationHandler<DomainNotification> notifications,
            IBadgeService badgeService
        ) : base(notifications)
        {
            _badgeService = badgeService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all badges")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<BadgeViewModel>>))]
        public async Task<IActionResult> GetAllBadgesAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<BadgeViewModelSortProvider, BadgeViewModel, Badge>]
            SortQuery? sortQuery = null
        )
        {
            var badges = await _badgeService.GetAllBadgesAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery
            );
            return Response(badges);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a badge by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<BadgeViewModel>))]
        public async Task<IActionResult> GetBadgeByIdAsync([FromRoute] Guid id)
        {
            var badge = await _badgeService.GetBadgeByIdAsync(id);
            return Response(badge);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        [AllowAnonymous]
        [SwaggerOperation("Create a new badge")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> CreateBadgeAsync([FromBody] CreateBadgeViewModel viewModel)
        {
            var badgeId = await _badgeService.CreateBadgeAsync(viewModel);
            return Response(badgeId);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        [SwaggerOperation("Delete a badge")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> DeleteBadgeAsync([FromRoute] Guid id)
        {
            await _badgeService.DeleteBadgeAsync(new DeleteBadgeViewModel(id));
            return Response(id);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut]
        [SwaggerOperation("Update a badge")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateBadgeViewModel>))]
        public async Task<IActionResult> UpdateBadgeAsync([FromBody] UpdateBadgeViewModel viewModel)
        {
            await _badgeService.UpdateBadgeAsync(viewModel);
            return Response(viewModel);
        }
    }
}
