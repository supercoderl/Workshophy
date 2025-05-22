using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class AuditLog : Entity
    {
        public Guid AdminId { get; private set; }
        public string Action { get; private set; }
        public string Target { get; private set; } // ví dụ: User, Workshop, Review
        public DateTime Timestamp { get; private set; }

        public AuditLog(
            Guid id, 
            Guid adminId, 
            string action, 
            string target
        ) : base(id)
        {
            AdminId = adminId;
            Action = action;
            Target = target;
            Timestamp = DateTime.UtcNow;
        }

        public void SetAdminId( Guid adminId ) { AdminId = adminId; }
        public void SetAction( string action ) { Action = action; }
        public void SetTarget( string target ) { Target = target; }
        public void SetTimestamp( DateTime timestamp ) { Timestamp = timestamp; }
    }
}
