using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.SortProviders;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels.Users;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Presentation.Models;
using WorkshopHub.Presentation.Swagger;
using WorkshopHub.Application.ViewModels.Workshops;
using WorkshopHub.Domain.Enums;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Presentation.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class WorkshopController : ApiController
    {
        private readonly IWorkshopService _workshopService;

        public WorkshopController(
            INotificationHandler<DomainNotification> notifications,
            IWorkshopService workshopService
        ) : base(notifications)
        {
            _workshopService = workshopService;
        }

        [AllowAnonymous]
        [HttpGet]
        [SwaggerOperation("Get a list of all workshops")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<WorkshopViewModel>>))]
        public async Task<IActionResult> GetAllWorkshopsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] WorkshopStatus status = WorkshopStatus.Approved,
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<WorkshopViewModelSortProvider, WorkshopViewModel, Workshop>]
            SortQuery? sortQuery = null,
            [FromQuery] WorkshopFilter? filter = null
        )
        {
            var workshops = await _workshopService.GetAllWorkshopsAsync(
                query,
                includeDeleted,
                searchTerm,
                status,
                sortQuery,
                filter
            );
            return Response(workshops);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [SwaggerOperation("Get a workshop by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<WorkshopViewModel>))]
        public async Task<IActionResult> GetWorkshopByIdAsync([FromRoute] Guid id)
        {
            var workshop = await _workshopService.GetWorkshopByIdAsync(id);
            return Response(workshop);
        }

        [Authorize(Roles = "Organizer")]
        [HttpPost]
        [SwaggerOperation("Create a new workshop")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> CreateWorkshopAsync([FromBody] CreateWorkshopViewModel viewModel)
        {
            var workshopId = await _workshopService.CreateWorkshopAsync(viewModel);
            return Response(workshopId);
        }

        [Authorize(Roles = "Organizer")]
        [HttpDelete("{id}")]
        [SwaggerOperation("Delete a workshop")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> DeleteWorkshopAsync([FromRoute] Guid id)
        {
            await _workshopService.DeleteWorkshopAsync(new DeleteWorkshopViewModel(id));
            return Response(id);
        }

        [Authorize(Roles = "Organizer")]
        [HttpPut]
        [SwaggerOperation("Update a workshop")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateWorkshopViewModel>))]
        public async Task<IActionResult> UpdateWorkshopAsync([FromBody] UpdateWorkshopViewModel viewModel)
        {
            await _workshopService.UpdateWorkshopAsync(viewModel);
            return Response(viewModel);
        }
    }
}
