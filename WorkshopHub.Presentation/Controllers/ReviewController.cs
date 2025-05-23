using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.SortProviders;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Reviews;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Presentation.Models;
using WorkshopHub.Presentation.Swagger;

namespace WorkshopHub.Presentation.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class ReviewController : ApiController
    {
        private readonly IReviewService _reviewService;

        public ReviewController(
            INotificationHandler<DomainNotification> notifications,
            IReviewService reviewService
        ) : base(notifications)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all reviews")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<ReviewViewModel>>))]
        public async Task<IActionResult> GetAllReviewsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] ReviewFilter? filter = null,
            [FromQuery] [SortableFieldsAttribute<ReviewViewModelSortProvider, ReviewViewModel, Review>]
            SortQuery? sortQuery = null
        )
        {
            var reviews = await _reviewService.GetAllReviewsAsync(
                query,
                includeDeleted,
                searchTerm,
                filter,
                sortQuery,
                false
            );
            return Response(reviews);
        }

        [Authorize(Roles = "Organizer")]
        [HttpGet("organizer/my")]
        [SwaggerOperation("Get a list of all reviews for organizer")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<ReviewViewModel>>))]
        public async Task<IActionResult> GetAllReviewsForOrganizerAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] ReviewFilter? filter = null,
            [FromQuery] [SortableFieldsAttribute<ReviewViewModelSortProvider, ReviewViewModel, Review>]
                    SortQuery? sortQuery = null
        )
        {
            var reviews = await _reviewService.GetAllReviewsAsync(
                query,
                includeDeleted,
                searchTerm,
                filter,
                sortQuery,
                true
            );
            return Response(reviews);
        }

        [HttpGet("workshop/{id}")]
        [SwaggerOperation("Get a list of all reviews by organizer")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<ReviewViewModel>>))]
        public async Task<IActionResult> GetAllReviewsByOrganizerAsync(
            Guid id,
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] ReviewFilter? filter = null,
            [FromQuery] [SortableFieldsAttribute<ReviewViewModelSortProvider, ReviewViewModel, Review>]
                    SortQuery? sortQuery = null
        )
        {
            var reviews = await _reviewService.GetAllReviewsAsync(
                query,
                includeDeleted,
                searchTerm,
                new ReviewFilter
                {
                    Star = filter?.Star,
                    HelpfulCount = filter?.HelpfulCount,
                    Date = filter?.Date,
                    WorkshopId = id
                },
                sortQuery,
                true
            );
            return Response(reviews);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a review by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<ReviewViewModel>))]
        public async Task<IActionResult> GetReviewByIdAsync([FromRoute] Guid id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            return Response(review);
        }

        [HttpPost]
        [SwaggerOperation("Create a new review")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> CreateReviewAsync([FromBody] CreateReviewViewModel viewModel)
        {
            var reviewId = await _reviewService.CreateReviewAsync(viewModel);
            return Response(reviewId);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Delete a review")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> DeleteReviewAsync([FromRoute] Guid id)
        {
            await _reviewService.DeleteReviewAsync(new DeleteReviewViewModel(id));
            return Response(id);
        }

        [HttpPut]
        [SwaggerOperation("Update a review")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateReviewViewModel>))]
        public async Task<IActionResult> UpdateReviewAsync([FromBody] UpdateReviewViewModel viewModel)
        {
            await _reviewService.UpdateReviewAsync(viewModel);
            return Response(viewModel);
        }
    }
}
