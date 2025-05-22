using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Helpers;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Domain.Settings;

namespace WorkshopHub.Domain.Commands.Users.RefreshToken
{
    public sealed class RefreshTokenCommandHandler : CommandHandlerBase, IRequestHandler<RefreshTokenCommand, object>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly TokenSettings _tokenSettings;
        private const double _expiryDurationMinutes = 60;
        private const int _expiryDurationDays = 45;

        public RefreshTokenCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            IOptions<TokenSettings> tokenSettings
        ) : base(bus, unitOfWork, notifications)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<object> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return string.Empty;

            var refreshToken = await _refreshTokenRepository.GetByToken(request.Token);

            if(refreshToken == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any token with code: {request.Token}",
                    ErrorCodes.ObjectNotFound
                ));
                return string.Empty;
            }

            var user = await _userRepository.GetByIdAsync(refreshToken.UserId);

            if (user == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any user with id: {refreshToken.UserId}",
                    ErrorCodes.ObjectNotFound
                ));
                return string.Empty;
            }

            var newRefreshToken = await TokenHelper.GenerateRefreshToken(user.Id, Bus, _expiryDurationDays);

            return new
            {
                AccessToken = TokenHelper.BuildToken(user, _tokenSettings, _expiryDurationMinutes),
                RefreshToken = newRefreshToken
            };
        }
    }
}
