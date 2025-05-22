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
    public sealed class PasswordResetTokenRepository : BaseRepository<PasswordResetToken>, IPasswordResetTokenRepository
    {
        public PasswordResetTokenRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<PasswordResetToken?> GetByToken(string otp)
        {
            return await DbSet.Where(p => p.Otp == otp).SingleOrDefaultAsync();
        }
    }
}
