using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class Badge : Entity
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string Area { get; private set; } // Arts, Business, Tech...
        public string ImageUrl { get; private set; }

        [InverseProperty("Badge")]
        public virtual ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();

        public Badge(
            Guid id, 
            string name, 
            string? description, 
            string area,
            string imageUrl
        ) : base(id)
        {
            Name = name;
            Description = description;
            Area = area;
            ImageUrl = imageUrl;
        }

        public void SetName(string name) { Name = name; }
        public void SetDescription(string? description) { Description = description; }
        public void SetArea(string area) { Area = area; }
        public void SetImageUrl(string imageUrl) { ImageUrl = imageUrl; }
    }
}
