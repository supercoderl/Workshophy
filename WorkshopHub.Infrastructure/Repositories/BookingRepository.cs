using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Infrastructure.Database;

namespace WorkshopHub.Infrastructure.Repositories
{
    public sealed class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<Booking?> GetByOrderCode(long orderCode)
        {
            return await DbSet
                .Include(b => b.User)
                .Include(b => b.Workshop)
                    .ThenInclude(w => w!.User)
                .Where(b => b.OrderCode == orderCode)
                .SingleOrDefaultAsync();
        }
    }
}
