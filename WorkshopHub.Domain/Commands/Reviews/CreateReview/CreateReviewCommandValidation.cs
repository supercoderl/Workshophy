using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Reviews.CreateReview
{
    public sealed class CreateReviewCommandValidation : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidation()
        {
            
        }
    }
}
