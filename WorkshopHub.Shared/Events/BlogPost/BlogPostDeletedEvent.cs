using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.BlogPost
{
    public sealed class BlogPostDeletedEvent : DomainEvent
    {
        public BlogPostDeletedEvent(Guid blogPostId) : base(blogPostId)
        {
            
        }
    }
}
