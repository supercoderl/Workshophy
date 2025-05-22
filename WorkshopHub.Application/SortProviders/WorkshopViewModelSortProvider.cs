using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels.Workshops;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.SortProviders
{
    public sealed class WorkshopViewModelSortProvider : ISortingExpressionProvider<WorkshopViewModel, Workshop>
    {
        private static readonly Dictionary<string, Expression<Func<Workshop, object>>> s_expressions = new()
        {
            { "title", workshop => workshop.Title },
            { "location", workshop => workshop.Location },
            { "price", workshop => workshop.Price },
            { "rating", workshop => workshop.Reviews },
            { "status", workshop => workshop.Status }
        };

        public Dictionary<string, Expression<Func<Workshop, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
