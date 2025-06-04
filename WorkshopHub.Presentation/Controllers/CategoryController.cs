using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.SortProviders;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Categories;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Presentation.Models;
using WorkshopHub.Presentation.Swagger;

namespace WorkshopHub.Presentation.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class CategoryController : ApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(
            INotificationHandler<DomainNotification> notifications,
            ICategoryService categoryService
        ) : base(notifications)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [SwaggerOperation("Get a list of all categories")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<CategoryViewModel>>))]
        public async Task<IActionResult> GetAllCategoriesAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<CategoryViewModelSortProvider, CategoryViewModel, Category>]
            SortQuery? sortQuery = null
        )
        {
            var categories = await _categoryService.GetAllCategoriesAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery
            );
            return Response(categories);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get a category by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<CategoryViewModel>))]
        public async Task<IActionResult> GetCategoryByIdAsync([FromRoute] Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Response(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [SwaggerOperation("Create a new category")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryViewModel viewModel)
        {
            var categoryId = await _categoryService.CreateCategoryAsync(viewModel);
            return Response(categoryId);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation("Delete a category")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Response(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [SwaggerOperation("Update a category")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateCategoryViewModel>))]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryViewModel viewModel)
        {
            await _categoryService.UpdateCategoryAsync(viewModel);
            return Response(viewModel);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("favourite")]
        [SwaggerOperation("Handle favourite")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> HandleFavouriteAsync([FromBody] HandleFavouriteViewModel viewModel)
        {
            await _categoryService.HandleFavouriteAsync(viewModel);
            return Response(Guid.NewGuid());
        }
    }
}
