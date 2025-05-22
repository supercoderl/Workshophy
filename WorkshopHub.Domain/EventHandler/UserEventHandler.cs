using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Shared.Events.PasswordResetToken;
using WorkshopHub.Shared.Events.User;

namespace WorkshopHub.Domain.EventHandler
{
    public sealed class UserEventHandler :
        INotificationHandler<UserDeletedEvent>,
        INotificationHandler<UserCreatedEvent>,
        INotificationHandler<UserUpdatedEvent>,
        INotificationHandler<PasswordResetTokenCreatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public UserEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
/*            await _distributedCache.RemoveAsync(
                CacheKeyGenerator.GetEntityCacheKey<User>(notification.AggregateId),
                cancellationToken);*/
        }

        public async Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            /*            await _distributedCache.RemoveAsync(
                            CacheKeyGenerator.GetEntityCacheKey<User>(notification.AggregateId),
                            cancellationToken);*/
        }

        public async Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            /*            await _distributedCache.RemoveAsync(
                            CacheKeyGenerator.GetEntityCacheKey<User>(notification.AggregateId),
                            cancellationToken);*/
        }

        public async Task Handle(PasswordResetTokenCreatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            /*            await _distributedCache.RemoveAsync(
                             CacheKeyGenerator.GetEntityCacheKey<PasswordResetToken>(notification.AggregateId),
                             cancellationToken);*/
        }
    }
}
