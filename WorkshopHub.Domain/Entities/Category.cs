using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }

        [InverseProperty("Category")]
        public virtual ICollection<Workshop> Workshops { get; set; } = new List<Workshop>();

        [InverseProperty("Category")]
        public virtual ICollection<UserInterest> UserInterests { get; set; } = new List<UserInterest>();

        public Category(
            Guid id,
            string name,
            string? description
        ) : base(id)
        {
            Name = name;
            Description = description;
        }

        public void SetName(string name) { Name = name; }
        public void SetDescription(string? description) { Description = description; }
    }
}
