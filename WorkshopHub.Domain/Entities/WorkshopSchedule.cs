using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class WorkshopSchedule : Entity
    {
        public Guid WorkshopId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        [ForeignKey("WorkshopId")]
        [InverseProperty("WorkshopSchedules")]
        public virtual Workshop? Workshop { get; set; }

        public WorkshopSchedule(
            Guid id,
            Guid workshopId,
            DateTime startTime,
            DateTime endTime
        ) : base(id)
        {
            WorkshopId = workshopId;
            StartTime = startTime;
            EndTime = endTime;
        }

        public void SetWorkshopId( Guid workshopId ) { WorkshopId = workshopId; }
        public void SetStartTime( DateTime startTime ) { StartTime = startTime; }
        public void SetEndTime( DateTime endTime ) { EndTime = endTime; }
    }
}
