using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class ActivityPointRule : Entity
    {
        public string ActivityType { get; private set; }
        public int ActivityPoint { get; private set; }

        public ActivityPointRule(
            Guid id,
            string activityType,
            int activityPoint
        ) : base(id)
        {
            ActivityType = activityType;
            ActivityPoint = activityPoint;
        }

        public void SetActivityType( string activityType ) {  ActivityType = activityType; }
        public void SetActivityPoint( int activityPoint ) { ActivityPoint = activityPoint; }
    }
}
