using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.BlogPosts.CreateBlogPost
{
    public sealed class CreateBlogPostCommandValidation : AbstractValidator<CreateBlogPostCommand>
    {
        public CreateBlogPostCommandValidation()
        {
            RuleForTitle();
            RuleForContent();
            RuleForUserId();
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
