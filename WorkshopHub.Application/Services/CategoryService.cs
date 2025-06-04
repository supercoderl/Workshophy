using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.Queries.Categories.GetAll;
using WorkshopHub.Application.Queries.Categories.GetCategoryById;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Categories;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Domain.Commands.Categories.CreateCategory;
using WorkshopHub.Domain.Commands.Categories.DeleteCategory;
using WorkshopHub.Domain.Commands.Categories.HandleFavourite;
using WorkshopHub.Domain.Commands.Categories.UpdateCategory;
using WorkshopHub.Domain.Interfaces;

namespace WorkshopHub.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMediatorHandler _bus;

        public CategoryService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreateCategoryAsync(CreateCategoryViewModel category)
        {
            var categoryId = Guid.NewGuid();
            await _bus.SendCommandAsync(new CreateCategoryCommand(
                categoryId,
                category.Name,
                category.Description
            ));
            return categoryId;
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            await _bus.SendCommandAsync(new DeleteCategoryCommand(categoryId));
        }

        public async Task<PagedResult<CategoryViewModel>> GetAllCategoriesAsync(PageQuery query, bool includeDeleted, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllCategoriesQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<CategoryViewModel?> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _bus.QueryAsync(new GetCategoryByIdQuery(categoryId));
        }

        public async Task HandleFavouriteAsync(HandleFavouriteViewModel handleFavourite)
        {
            await _bus.SendCommandAsync(new HandleFavouriteCommand(handleFavourite.CategoryIds));
        }

        public async Task UpdateCategoryAsync(UpdateCategoryViewModel category)
        {
            await _bus.SendCommandAsync(new UpdateCategoryCommand(
                category.CategoryId,
                category.Name,
                category.Description
            ));
        }
    }
}
