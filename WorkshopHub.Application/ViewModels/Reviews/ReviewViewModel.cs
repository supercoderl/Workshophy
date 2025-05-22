using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.ViewModels.Reviews
{
    public sealed class ReviewViewModel
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public Guid WorkshopId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public static ReviewViewModel FromReview(Review model)
        {
            return new ReviewViewModel
            {
                ReviewId = model.Id,
                UserId = model.UserId,
                WorkshopId = model.WorkshopId,
                Rating = model.Rating,
                CreatedAt = model.CreatedAt,
                Comment = model.Comment
            };
        }
    }
}
