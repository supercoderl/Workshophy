using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Domain.Entities
{
    public class Workshop : Entity
    {
        public Guid OrganizerId { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public Guid CategoryId { get; private set; }
        public string Location { get; private set; }
        public string? IntroVideoUrl { get; private set; }
        public int DurationMinutes { get; private set; }
        public decimal Price { get; private set; }
        public WorkshopStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        [InverseProperty("Workshop")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        [InverseProperty("Workshop")]
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        [InverseProperty("Workshop")]
        public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();

        [InverseProperty("Workshop")]
        public virtual ICollection<WorkshopPromotion> WorkshopPromotions { get; set; } = new List<WorkshopPromotion>();

        [InverseProperty("Workshop")]
        public virtual ICollection<WorkshopSchedule> WorkshopSchedules { get; set; } = new List<WorkshopSchedule>();

        [InverseProperty("Workshop")]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        [ForeignKey("CategoryId")]
        [InverseProperty("Workshops")]
        public virtual Category? Category { get; set; }

        public Workshop(
            Guid id,
            Guid organizerId,
            string title,
            string? description,
            Guid categoryId,
            string location,
            int durationMinutes,
            decimal price,
            WorkshopStatus status
        ) : base(id)
        {
            OrganizerId = organizerId;
            Title = title;
            Description = description;
            CategoryId = categoryId;
            Location = location;
            DurationMinutes = durationMinutes;
            Price = price;
            Status = status;
        }

        public void SetOrganizerId(Guid organizerId) { OrganizerId = organizerId; }
        public void SetTitle(string title) { Title = title; }
        public void SetDescription(string? description) { Description = description; }
        public void SetCategoryId(Guid categoryId) { CategoryId = categoryId; }
        public void SetLocation(string location) { Location = location; }
        public void SetDurationMinutes(int durationMinutes) { DurationMinutes = durationMinutes; }
        public void SetPrice(decimal price) { Price = price; }
        public void SetIntroVideoUrl(string? introVideoUrl) { IntroVideoUrl = introVideoUrl; }
        public void SetStatus(WorkshopStatus status) { Status = status; }
    }
}
