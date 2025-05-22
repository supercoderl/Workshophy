using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.ViewModels.BlogPosts
{
    public sealed class BlogPostViewModel
    {
        public Guid BlogPostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content {  get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public static BlogPostViewModel FromBlogPost(BlogPost blogPost)
        {
            return new BlogPostViewModel
            {
                BlogPostId = blogPost.Id,
                Title = blogPost.Title,
                Content = blogPost.Content,
                CreatedAt = blogPost.CreatedAt,
                UserId = blogPost.UserId
            };
        }
    }
}
