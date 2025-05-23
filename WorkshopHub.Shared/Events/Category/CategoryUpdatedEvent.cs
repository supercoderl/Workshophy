using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.Category
{
    public sealed class CategoryUpdatedEvent : DomainEvent
    {
        public CategoryUpdatedEvent(Guid categoryId) : base(categoryId)
        {
            
        }
    }
}
