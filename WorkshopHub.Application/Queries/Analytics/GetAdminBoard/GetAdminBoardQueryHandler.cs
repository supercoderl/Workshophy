using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Analytics;
using WorkshopHub.Domain.Interfaces.Repositories;

namespace WorkshopHub.Application.Queries.Analytics.GetAdminBoard
{
    public sealed class GetAdminBoardQueryHandler : IRequestHandler<GetAdminBoardQuery, AdminBoardViewModel>
    {
        private readonly IUserRepository _userRepository;

        public GetAdminBoardQueryHandler(
            IUserRepository userRepository
        )
        {
            _userRepository = userRepository;
        }

        public async Task<AdminBoardViewModel> Handle(GetAdminBoardQuery request, CancellationToken cancellationToken)
        {
            var usersQuery = _userRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(x => x.DeletedAt == null);

            var totalUser = await usersQuery.CountAsync();
            var totalActiveUser = await usersQuery.Where(user => user.Status == Domain.Enums.UserStatus.Active).CountAsync();

            return AdminBoardViewModel.FromAdminBoard(totalUser, totalActiveUser);
        }
    }
}
