using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.BlogPosts;

namespace WorkshopHub.Application.Queries.BlogPosts.GetAll
{
    public sealed record GetAllBlogPostsQuery
    (
        PageQuery Query,
        bool IncludeDeleted,
        string SearchTerm = "",
        SortQuery? SortQuery = null) :
        IRequest<PagedResult<BlogPostViewModel>>;
}
