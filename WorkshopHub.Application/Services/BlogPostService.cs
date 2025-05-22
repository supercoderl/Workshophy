using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.Queries.Tickets.GetAll;
using WorkshopHub.Application.Queries.Tickets.GetTicketById;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels.Tickets;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Domain.Commands.Tickets.CreateTicket;
using WorkshopHub.Domain.Commands.Tickets.DeleteTicket;
using WorkshopHub.Domain.Commands.Tickets.UpdateTicket;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Application.ViewModels.BlogPosts;
using WorkshopHub.Domain.Commands.BlogPosts.CreateBlogPost;
using WorkshopHub.Domain.Commands.BlogPosts.DeleteBlogPost;
using WorkshopHub.Application.Queries.BlogPosts.GetAll;
using WorkshopHub.Application.Queries.BlogPosts.GetBlogPostById;
using WorkshopHub.Domain.Commands.BlogPosts.UpdateBlogPost;

namespace WorkshopHub.Application.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IMediatorHandler _bus;

        public BlogPostService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreateBlogPostAsync(CreateBlogPostViewModel blogPost)
        {
            var blogPostId = Guid.NewGuid();
            await _bus.SendCommandAsync(new CreateBlogPostCommand(blogPostId, blogPost.Title, blogPost.Content, blogPost.UserId));
            return blogPostId;
        }

        public async Task DeleteBlogPostAsync(DeleteBlogPostViewModel blogPost)
        {
            await _bus.SendCommandAsync(new DeleteBlogPostCommand(blogPost.BlogPostId));
        }

        public async Task<PagedResult<BlogPostViewModel>> GetAllBlogPostsAsync(PageQuery query, bool includeDeleted, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllBlogPostsQuery(
                query,
                includeDeleted,
                searchTerm,
                sortQuery
            ));
        }

        public async Task<BlogPostViewModel?> GetBlogPostByIdAsync(Guid blogPostId)
        {
            return await _bus.QueryAsync(new GetBlogPostByIdQuery(blogPostId));
        }

        public async Task UpdateBlogPostAsync(UpdateBlogPostViewModel blogPost)
        {
            await _bus.SendCommandAsync(new UpdateBlogPostCommand(
                blogPost.BlogPostId,
                blogPost.Title,
                blogPost.Content,
                blogPost.UserId
            ));
        }
    }
}
