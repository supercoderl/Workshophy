using MediatR;
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
using WorkshopHub.Shared.Events.User;
using BC = BCrypt.Net.BCrypt;

namespace WorkshopHub.Domain.Commands.Users.ResetPassword
{
    public sealed class ResetPasswordCommandHandler : CommandHandlerBase, IRequestHandler<ResetPasswordCommand>
    {
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly IUserRepository _userRepository;

        public ResetPasswordCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IPasswordResetTokenRepository passwordResetTokenRepository,
            IUserRepository userRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var token = await _passwordResetTokenRepository.GetByToken(request.OTP);

            if(token == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"OTP is invalid.",
                    ErrorCodes.InvalidObject
                ));
                return;
            }

            else if(token.ExpireAt <= TimeHelper.GetTimeNow())
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"This OTP was expired.",
                    ErrorCodes.InvalidObject
                ));
                return;
            }

            var user = await _userRepository.GetByIdAsync(token.UserId);

            if (user == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any user with id: {token.UserId}",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            user.SetPassword(BC.HashPassword(request.NewPassword));
            token.SetIsUsed(true);
            token.SetExpireAt(TimeHelper.GetTimeNow());

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new UserResetPasswordEvent(user.Id));
            };
        }
    }
}
