﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Infrastructure.Database;

namespace WorkshopHub.Infrastructure.Repositories
{
    public sealed class BadgeRepository : BaseRepository<Badge>, IBadgeRepository
    {
        public BadgeRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
