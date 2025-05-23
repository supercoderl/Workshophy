using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.Category
{
    public sealed class CategoryCreatedEvent : DomainEvent
    {
        public CategoryCreatedEvent(Guid categoryId) : base(categoryId)
        {
            
        }
    }
}
