using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Categories;

namespace WorkshopHub.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<CategoryViewModel?> GetCategoryByIdAsync(Guid categoryId);

        public Task<PagedResult<CategoryViewModel>> GetAllCategoriesAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null
        );

        public Task<Guid> CreateCategoryAsync(CreateCategoryViewModel category);
        public Task UpdateCategoryAsync(UpdateCategoryViewModel category);
        public Task DeleteCategoryAsync(Guid categoryId);
    }
}
