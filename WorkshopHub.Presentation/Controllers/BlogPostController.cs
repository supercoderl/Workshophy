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
using WorkshopHub.Application.ViewModels.BlogPosts;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/v1/[controller]")]
    public sealed class BlogPostController : ApiController
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostController(
            INotificationHandler<DomainNotification> notifications,
            IBlogPostService blogPostService
        ) : base(notifications)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all blogPosts")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<BlogPostViewModel>>))]
        public async Task<IActionResult> GetAllBlogPostsAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<BlogPostViewModelSortProvider, BlogPostViewModel, BlogPost>]
            SortQuery? sortQuery = null
        )
        {
            var blogPosts = await _blogPostService.GetAllBlogPostsAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery
            );
            return Response(blogPosts);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a blogPost by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<BlogPostViewModel>))]
        public async Task<IActionResult> GetBlogPostByIdAsync([FromRoute] Guid id)
        {
            var blogPost = await _blogPostService.GetBlogPostByIdAsync(id);
            return Response(blogPost);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [SwaggerOperation("Create a new blogPost")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> CreateBlogPostAsync([FromBody] CreateBlogPostViewModel viewModel)
        {
            var blogPostId = await _blogPostService.CreateBlogPostAsync(viewModel);
            return Response(blogPostId);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation("Delete a blogPost")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> DeleteBlogPostAsync([FromRoute] Guid id)
        {
            await _blogPostService.DeleteBlogPostAsync(new DeleteBlogPostViewModel(id));
            return Response(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [SwaggerOperation("Update a blogPost")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateBlogPostViewModel>))]
        public async Task<IActionResult> UpdateBlogPostAsync([FromBody] UpdateBlogPostViewModel viewModel)
        {
            await _blogPostService.UpdateBlogPostAsync(viewModel);
            return Response(viewModel);
        }
    }
}
