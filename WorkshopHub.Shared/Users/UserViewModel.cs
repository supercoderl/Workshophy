﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Users
{
    public sealed record UserViewModel(
        Guid Id,
        string Email,
        string FirstName,
        string LastName,
        string PhoneNumber,
        int AchievementPoint,
        DateTimeOffset? DeletedAt
    );
}
