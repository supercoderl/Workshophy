﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Domain.Interfaces.Repositories
{
    public interface IWorkshopRepository : IRepository<Workshop>
    {
        IQueryable<Workshop> GetByCategories(ICollection<Guid> categoryIds);
        Task<IEnumerable<Guid>> GetIdsByUserId(Guid userId);
    }
}
