using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Application.ViewModels.Workshops
{
    public sealed class WorkshopViewModel
    {
        public Guid WorkshopId { get; set; }
        public Guid OrganizerId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? IntroVideoUrl { get; set; }
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
        public WorkshopStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public static WorkshopViewModel FromWorkshop(Workshop workshop)
        {
            return new WorkshopViewModel
            {
                WorkshopId = workshop.Id,
                OrganizerId = workshop.OrganizerId,
                Title = workshop.Title,
                Description = workshop.Description,
                CategoryId = workshop.CategoryId,
                Location = workshop.Location,
                IntroVideoUrl = workshop.IntroVideoUrl,
                DurationMinutes = workshop.DurationMinutes,
                Price = workshop.Price,
                Status = workshop.Status,
                CreatedAt = workshop.CreatedAt,
            };
        }
    }
}
