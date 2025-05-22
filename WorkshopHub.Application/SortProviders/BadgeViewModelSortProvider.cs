using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Badges;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.SortProviders
{
    public sealed class BadgeViewModelSortProvider : ISortingExpressionProvider<BadgeViewModel, Badge>
    {
        private static readonly Dictionary<string, Expression<Func<Badge, object>>> s_expressions = new()
        {
            { "name", badge => badge.Name },
            { "description", badge => badge.Description ?? string.Empty },
            { "area", badge => badge.Area },
        };

        public Dictionary<string, Expression<Func<Badge, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
