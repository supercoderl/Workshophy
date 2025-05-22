using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.BlogPosts.CreateBlogPost
{
    public sealed class CreateBlogPostCommand : CommandBase, IRequest
    {
        private static readonly CreateBlogPostCommandValidation s_validation = new();

        public Guid BlogPostId { get; }
        public string Title { get; }
        public string Content { get; }
        public Guid UserId { get; }

        public CreateBlogPostCommand(
            Guid blogPostId,
            string title,
            string content,
            Guid userId
        ) : base(Guid.NewGuid())
        {
            BlogPostId = blogPostId;
            Title = title;
            Content = content;
            UserId = userId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
