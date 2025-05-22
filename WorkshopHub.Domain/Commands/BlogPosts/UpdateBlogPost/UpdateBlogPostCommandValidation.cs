using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.BlogPosts.UpdateBlogPost
{
    public sealed class UpdateBlogPostCommandValidation : AbstractValidator<UpdateBlogPostCommand>
    {
        public UpdateBlogPostCommandValidation()
        {
            RuleForId();
            RuleForTitle();
            RuleForContent();
            RuleForUserId();
        }

        public void RuleForId()
        {
            RuleFor(cmd => cmd.BlogPostId).NotEmpty().WithErrorCode(DomainErrorCodes.BlogPost.EmptyId).WithMessage("Id may not be empty.");
        }

        public void RuleForTitle()
        {
            RuleFor(cmd => cmd.Title).NotEmpty().WithErrorCode(DomainErrorCodes.BlogPost.EmptyTitle).WithMessage("Title may not be empty.");
        }

        public void RuleForContent()
        {
            RuleFor(cmd => cmd.Content).NotEmpty().WithErrorCode(DomainErrorCodes.BlogPost.EmptyContent).WithMessage("Content may not be empty.");
        }

        public void RuleForUserId()
        {
            RuleFor(cmd => cmd.UserId).NotEmpty().WithErrorCode(DomainErrorCodes.BlogPost.EmptyUserId).WithMessage("User id may not be empty");
        }
    }
}
