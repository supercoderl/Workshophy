using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Domain.Settings;
using BC = BCrypt.Net.BCrypt;
using WorkshopHub.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using WorkshopHub.Domain.Helpers;

namespace WorkshopHub.Domain.Commands.Users.Login
{
    public sealed class LoginCommandHandler : CommandHandlerBase,
        IRequestHandler<LoginCommand, object>
    {
        private const double _expiryDurationMinutes = 60;
        private const int _expiryDurationDays = 45;
        private readonly TokenSettings _tokenSettings;

        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IUserRepository userRepository,
            IOptions<TokenSettings> tokenSettings) : base(bus, unitOfWork, notifications)
        {
            _userRepository = userRepository;
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<object> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return string.Empty;
            }

            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no user with email {request.Email}",
                        ErrorCodes.ObjectNotFound));

                return string.Empty;
            }

            var passwordVerified = BC.Verify(request.Password, user.Password);

            if (!passwordVerified)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        "The password is incorrect",
                        DomainErrorCodes.User.PasswordIncorrect));

                return string.Empty;
            }

            user.SetLastLoggedinDate(TimeHelper.GetTimeNow());

            if (!await CommitAsync()) return string.Empty;

            var refreshToken = await TokenHelper.GenerateRefreshToken(user.Id, Bus, _expiryDurationDays);

            if (string.IsNullOrEmpty(refreshToken)) return string.Empty;

            return new
            {
                AccessToken = TokenHelper.BuildToken(
                    user,
                    _tokenSettings,
                    _expiryDurationMinutes
                ),
                RefreshToken = refreshToken
            };
        }
    }
}
