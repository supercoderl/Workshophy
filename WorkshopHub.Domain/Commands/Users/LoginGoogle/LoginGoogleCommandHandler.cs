using Duende.IdentityModel.Client;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Commands.Users.CreateUser;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Helpers;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Domain.Settings;

namespace WorkshopHub.Domain.Commands.Users.LoginGoogle
{
    public sealed class LoginGoogleCommandHandler : CommandHandlerBase, IRequestHandler<LoginGoogleCommand, object>
    {
        private const double _expiryDurationMinutes = 60;
        private const int _expiryDurationDays = 45;
        private readonly GoogleSettings _googleSettings;
        private readonly TokenSettings _tokenSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserRepository _userRepository;

        public LoginGoogleCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IOptions<GoogleSettings> googleSettings,
            IHttpClientFactory httpClientFactory,
            IUserRepository userRepository,
            IOptions<TokenSettings> tokenSettings
        ) : base(bus, unitOfWork, notifications)
        {
            _googleSettings = googleSettings.Value;
            _tokenSettings = tokenSettings.Value;
            _httpClientFactory = httpClientFactory;
            _userRepository = userRepository;
        }

        public async Task<object> Handle(LoginGoogleCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return string.Empty;

            var client = _httpClientFactory.CreateClient();
            var tokenResponse = await client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
            {
                Address = "https://oauth2.googleapis.com/token",
                ClientId = _googleSettings.ClientId,
                ClientSecret = _googleSettings.ClientSecret,
                Code = request.Code,
                RedirectUri = request.RedirectUri
            });

            if (tokenResponse.IsError)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"An error occured when verifying authorization code. Detail: {tokenResponse.Error}",
                    ErrorCodes.InvalidObject
                ));
                return string.Empty;
            }

            var userInfoResponse = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = "https://openidconnect.googleapis.com/v1/userinfo",
                Token = tokenResponse.AccessToken
            });

            if (userInfoResponse.IsError)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"An error occured when getting user information. Detail: {userInfoResponse.Error}",
                    ErrorCodes.InvalidObject
                ));
                return string.Empty;
            }

            var email = userInfoResponse.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            if(string.IsNullOrEmpty(email))
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"Email does not exists",
                    ErrorCodes.ObjectNotFound
                ));
                return string.Empty;
            }

            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                user = new Entities.User(
                    Guid.NewGuid(),
                    email,
                    "",
                    "",
                    "Password123!",
                    "",
                    0,
                    Enums.UserRole.Customer,
                    Enums.UserStatus.PendingApproval
                );

                await Bus.SendCommandAsync(new CreateUserCommand(
                    user.Id,
                    user.Email,
                    user.FirstName,
                    user.LastName,
                    user.Password,
                    user.PhoneNumber,
                    user.Role,
                    user.Status
                ));
            }

            var refreshToken = await TokenHelper.GenerateRefreshToken(user.Id, Bus, _expiryDurationDays);

            return new
            {
                AccessToken = TokenHelper.BuildToken(user, _tokenSettings, _expiryDurationMinutes),
                RefreshToken = refreshToken,
            };
        }
    }
}
