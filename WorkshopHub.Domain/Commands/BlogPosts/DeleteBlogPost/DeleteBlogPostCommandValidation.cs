using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.BlogPosts.DeleteBlogPost
{
    public sealed class DeleteBlogPostCommandValidation : AbstractValidator<DeleteBlogPostCommand>
    {
        public DeleteBlogPostCommandValidation()
        {
            RuleForId();
        }

        public void RuleForId()
        {
            RuleFor(cmd => cmd.BlogPostId).NotEmpty().WithErrorCode(DomainErrorCodes.BlogPost.EmptyId).WithMessage("Id may not be empty.");
        }
    }
}
