using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Domain.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        UserRole GetUserRole();
        string GetUserEmail();
    }
}
