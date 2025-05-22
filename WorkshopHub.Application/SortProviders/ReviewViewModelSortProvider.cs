using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Reviews;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.SortProviders
{
    public sealed class ReviewViewModelSortProvider : ISortingExpressionProvider<ReviewViewModel, Review>
    {
        private static readonly Dictionary<string, Expression<Func<Review, object>>> s_expressions = new()
        {

        };

        public Dictionary<string, Expression<Func<Review, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
