using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.BlogPosts;

namespace WorkshopHub.Application.Queries.BlogPosts.GetBlogPostById
{
    public sealed record GetBlogPostByIdQuery(Guid Id) : IRequest<BlogPostViewModel?>;
}
