using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class UserInterest : Entity
    {
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("UserInterests")]
        public virtual User? User { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("UserInterests")]
        public virtual Category? Category { get; set; }

        public UserInterest(
            Guid id,
            Guid userId,
            Guid categoryId
        ) : base(id)
        {
            UserId = userId;
            CategoryId = categoryId;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetCategoryId( Guid categoryId ) { CategoryId = categoryId; }
    }
}
