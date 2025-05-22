using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Shared.Events.RefreshToken;

namespace WorkshopHub.Domain.EventHandler
{
    public sealed class RefreshTokenEventHandler :
            INotificationHandler<RefreshTokenCreatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public RefreshTokenEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task Handle(RefreshTokenCreatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            /*            await _distributedCache.RemoveAsync(
                            CacheKeyGenerator.GetEntityCacheKey<RefreshToken>(notification.AggregateId),
                            cancellationToken);*/
        }
    }
}
