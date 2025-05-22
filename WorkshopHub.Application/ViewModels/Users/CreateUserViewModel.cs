using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Application.ViewModels.Users
{
    public sealed record CreateUserViewModel(
        string Email,
        string FirstName,
        string LastName,
        string Password,
        UserRole UserRole,
        UserStatus Status
    );
}
