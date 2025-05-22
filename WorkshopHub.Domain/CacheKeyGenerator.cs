using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Domain
{
    public static class CacheKeyGenerator
    {
        public static string GetEntityCacheKey<TEntity>(TEntity entity) where TEntity : Entity
        {
            return $"{typeof(TEntity)}-{entity.Id}";
        }

        public static string GetEntityCacheKey<TEntity>(Guid id) where TEntity : Entity
        {
            return $"{typeof(TEntity)}-{id}";
        }
    }
}
