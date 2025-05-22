using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.BlogPosts.UpdateBlogPost
{
    public sealed class UpdateBlogPostCommand : CommandBase, IRequest
    {
        private static readonly UpdateBlogPostCommandValidation s_validation = new();

        public Guid BlogPostId { get; }
        public string Title { get; }
        public string Content { get; }
        public Guid UserId { get; }

        public UpdateBlogPostCommand(
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
