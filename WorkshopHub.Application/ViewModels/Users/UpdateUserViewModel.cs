using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Application.ViewModels.Users
{
    public sealed record UpdateUserViewModel(
        Guid Id,
        string Email,
        string FirstName,
        string LastName,
        UserRole Role,
        UserStatus Status
    );
}
