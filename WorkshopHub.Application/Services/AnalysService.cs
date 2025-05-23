using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.Queries.Analytics.GetAdminBoard;
using WorkshopHub.Application.Queries.Analytics.GetOrganizerBoard;
using WorkshopHub.Application.ViewModels.Analytics;
using WorkshopHub.Domain.Interfaces;

namespace WorkshopHub.Application.Services
{
    public class AnalysService : IAnalysService
    {
        private readonly IMediatorHandler _bus;

        public AnalysService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<AdminBoardViewModel?> GetAdminBoardAsync()
        {
            return await _bus.QueryAsync(new GetAdminBoardQuery());
        }

        public async Task<OrganizerBoardViewModel?> GetOrganizerBoardAsync()
        {
            return await _bus.QueryAsync(new GetOrganizerBoardQuery());
        }
    }
}
