using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.BlogPosts.DeleteBlogPost
{
    public sealed class DeleteBlogPostCommand : CommandBase, IRequest
    {
        private static readonly DeleteBlogPostCommandValidation s_validation = new();

        public Guid BlogPostId { get; }

        public DeleteBlogPostCommand(Guid blogPostId) : base(Guid.NewGuid())
        {
            BlogPostId = blogPostId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
