using MediatR;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Application.ViewModels.BlogPosts;
using WorkshopHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WorkshopHub.Application.Extensions;

namespace WorkshopHub.Application.Queries.BlogPosts.GetAll
{
    public sealed class GetAllBlogPostsQueryHandler :
                IRequestHandler<GetAllBlogPostsQuery, PagedResult<BlogPostViewModel>>
    {
        private readonly ISortingExpressionProvider<BlogPostViewModel, BlogPost> _sortingExpressionProvider;
        private readonly IBlogPostRepository _blogPostRepository;

        public GetAllBlogPostsQueryHandler(
            IBlogPostRepository blogPostRepository,
            ISortingExpressionProvider<BlogPostViewModel, BlogPost> sortingExpressionProvider)
        {
            _blogPostRepository = blogPostRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
        }

        public async Task<PagedResult<BlogPostViewModel>> Handle(
            GetAllBlogPostsQuery request,
            CancellationToken cancellationToken)
        {
            var blogPostsQuery = _blogPostRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(x => request.IncludeDeleted || x.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {

            }

            var totalCount = await blogPostsQuery.CountAsync(cancellationToken);

            blogPostsQuery = blogPostsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            var blogPosts = await blogPostsQuery
                .Skip((request.Query.Page - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize)
                .Select(blogPost => BlogPostViewModel.FromBlogPost(blogPost))
                .ToListAsync(cancellationToken);

            return new PagedResult<BlogPostViewModel>(
                totalCount, blogPosts, request.Query.Page, request.Query.PageSize);
        }
    }
}
