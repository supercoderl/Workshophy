using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Reviews.UpdateReview
{
    public sealed class UpdateReviewCommand : CommandBase, IRequest
    {
        private static readonly UpdateReviewCommandValidation s_validation = new();

        public Guid ReviewId { get; }
        public Guid UserId { get; }
        public Guid WorkshopId { get; }
        public int Rating { get; }
        public string? Comment { get; }

        public UpdateReviewCommand(
            Guid reviewId,
            Guid userId,
            Guid workshopId,
            int rating,
            string? comment
        ) : base(Guid.NewGuid())
        {
            ReviewId = reviewId;
            UserId = userId;
            WorkshopId = workshopId;
            Rating = rating;
            Comment = comment;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
