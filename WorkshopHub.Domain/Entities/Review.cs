using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Helpers;

namespace WorkshopHub.Domain.Entities
{
    public class Review : Entity
    {
        public Guid UserId { get; private set; }
        public Guid WorkshopId { get; private set; }
        public int Rating { get; private set; }
        public string? Comment { get; private set; }
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("Reviews")]
        public virtual User? User { get; set; }

        [ForeignKey("WorkshopId")]
        [InverseProperty("Reviews")]
        public virtual Workshop? Workshop { get; set; }

        public Review(
            Guid id,
            Guid userId,
            Guid workshopId,
            int rating,
            string? comment
        ) : base(id)
        {
            UserId = userId;
            WorkshopId = workshopId;
            Rating = rating;
            Comment = comment;
            CreatedAt = TimeHelper.GetTimeNow();
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetWorkshopId( Guid workshopId ) { WorkshopId = workshopId; }
        public void SetRating( int rating ) { Rating = rating; }
        public void SetComment( string? comment ) { Comment = comment; }
    }
}
