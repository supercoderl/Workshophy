using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.ViewModels.Analytics
{
    public sealed class AdminBoardViewModel
    {
        public int TotalUser {  get; set; }
        public int TotalActiveUser { get; set; }
        public decimal RevenueByMonth { get; set; }
        public Guid MostAttendedWorkshop {  get; set; }

        public static AdminBoardViewModel FromAdminBoard(int totalUser, int totalActiveUser, List<decimal> revenueByMonths)
        {
            return new AdminBoardViewModel
            {
                TotalUser = totalUser,
                TotalActiveUser = totalActiveUser,
                RevenueByMonth = revenueByMonths.Sum(),
                MostAttendedWorkshop = Guid.Empty
            };
        }
    }
}
