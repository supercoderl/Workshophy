﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.Review
{
    public sealed class ReviewUpdatedEvent : DomainEvent
    {
        public ReviewUpdatedEvent(Guid reviewId) : base(reviewId)
        {
            
        }
    }
}
