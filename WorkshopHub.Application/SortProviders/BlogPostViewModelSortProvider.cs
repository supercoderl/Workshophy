using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WorkshopHub.Application.ViewModels.BlogPosts;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.SortProviders
{
    public sealed class BlogPostViewModelSortProvider : ISortingExpressionProvider<BlogPostViewModel, BlogPost>
    {
        private static readonly Dictionary<string, Expression<Func<BlogPost, object>>> s_expressions = new()
        {
            { "title", blogPost => blogPost.Title },
            { "content", blogPost => blogPost.Content }
        };

        public Dictionary<string, Expression<Func<BlogPost, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
