using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Analytics;

namespace WorkshopHub.Application.Interfaces
{
    public interface IAnalysService
    {
        public Task<AdminBoardViewModel?> GetAdminBoardAsync(string? month);
        public Task<OrganizerBoardViewModel?> GetOrganizerBoardAsync();
    }
}
