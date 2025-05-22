using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.ViewModels.Badges
{
    public sealed class BadgeViewModel
    {
        public Guid BadgeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Area { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public static BadgeViewModel FromBadge(Badge badge)
        {
            return new BadgeViewModel
            {
                BadgeId = badge.Id,
                Name = badge.Name,
                Description = badge.Description,
                Area = badge.Area,
                ImageUrl = badge.ImageUrl,
            };
        }
    }
}
