using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;

namespace WorkshopHub.Domain.Commands.Users.Logout
{
    public sealed class LogoutCommandHandler : CommandHandlerBase, IRequestHandler<LogoutCommand>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LogoutCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IRefreshTokenRepository refreshTokenRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var refreshToken = await _refreshTokenRepository.GetByToken(request.Token);

            if(refreshToken != null)
            {
                _refreshTokenRepository.Remove(refreshToken, true);

                await CommitAsync();
            }
        }
    }
}
