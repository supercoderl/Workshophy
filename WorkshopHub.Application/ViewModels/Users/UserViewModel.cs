using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Application.ViewModels.Users
{
    public sealed class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int AchievementPoint { get; set; }
        public string? AccountBank { get; set; }
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }

        public static UserViewModel FromUser(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                AchievementPoint = user.AchievementPoint,
                AccountBank = user.AccountBank,
                Role = user.Role,
                Status = user.Status
            };
        }
    }
}
