using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.BlogPost
{
    public sealed class BlogPostUpdatedEvent : DomainEvent
    {
        public BlogPostUpdatedEvent(Guid blogPostId) : base(blogPostId)
        {
            
        }
    }
}
