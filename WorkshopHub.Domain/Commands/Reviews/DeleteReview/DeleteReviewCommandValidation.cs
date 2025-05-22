using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Reviews.DeleteReview
{
    public sealed class DeleteReviewCommandValidation : AbstractValidator<DeleteReviewCommand>
    {
        public DeleteReviewCommandValidation()
        {
            RuleForId();
        }

        public void RuleForId()
        {
            RuleFor(cmd => cmd.ReviewId).NotEmpty().WithErrorCode(DomainErrorCodes.Review.EmptyId).WithMessage("Review id may not be empty.");
        }
    }
}
