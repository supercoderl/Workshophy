using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.ViewModels.Categories
{
    public sealed class CategoryViewModel
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public static CategoryViewModel FromCategory(Category category)
        {
            return new CategoryViewModel
            {
                CategoryId = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
        }
    };
}
