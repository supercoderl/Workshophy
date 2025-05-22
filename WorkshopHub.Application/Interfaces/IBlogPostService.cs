using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.BlogPosts;

namespace WorkshopHub.Application.Interfaces
{
    public interface IBlogPostService
    {
        public Task<BlogPostViewModel?> GetBlogPostByIdAsync(Guid blogPostId);

        public Task<PagedResult<BlogPostViewModel>> GetAllBlogPostsAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null
        );

        public Task<Guid> CreateBlogPostAsync(CreateBlogPostViewModel blogPost);
        public Task UpdateBlogPostAsync(UpdateBlogPostViewModel blogPost);
        public Task DeleteBlogPostAsync(DeleteBlogPostViewModel blogPost);
    }
}
