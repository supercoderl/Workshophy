using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WorkshopHub.Application.ViewModels.Categories;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.SortProviders
{
    public sealed class CategoryViewModelSortProvider : ISortingExpressionProvider<CategoryViewModel, Category>
    {
        private static readonly Dictionary<string, Expression<Func<Category, object>>> s_expressions = new()
        {
            { "name", category => category.Name }
        };

        public Dictionary<string, Expression<Func<Category, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
