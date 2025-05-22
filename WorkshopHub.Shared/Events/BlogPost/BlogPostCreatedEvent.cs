using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.BlogPost
{
    public sealed class BlogPostCreatedEvent : DomainEvent
    {
        public BlogPostCreatedEvent(Guid blogPostId) : base(blogPostId)
        {
            
        }
    }
}
