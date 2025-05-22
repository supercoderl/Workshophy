using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Commands.Mail.SendMail;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Helpers;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Models;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.PasswordResetToken;

namespace WorkshopHub.Domain.Commands.Users.ForgotPassword
{
    public sealed class ForgotPasswordCommandHandler : CommandHandlerBase, IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;

        public ForgotPasswordCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IUserRepository userRepository,
            IPasswordResetTokenRepository passwordResetTokenRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _userRepository = userRepository;
            _passwordResetTokenRepository = passwordResetTokenRepository;
        }

        public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var user = await _userRepository.GetByEmailAsync(request.Email);

            if(user == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no user with email: {request.Email}",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            var otp = TokenHelper.Generate6DigitToken();

            var passwordResetToken = new Entities.PasswordResetToken(
                Guid.NewGuid(),
                user.Id,
                otp,
                TimeHelper.GetTimeNow().AddMinutes(5),
                false
            );

            _passwordResetTokenRepository.Add( passwordResetToken );

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new PasswordResetTokenCreatedEvent(passwordResetToken.Id));

                // Send mail here
                await Bus.SendCommandAsync(new SendMailCommand(
                    user.Email,
                    $"Use This Code to Reset Your Password",
                    request.HtmlBody,
                    true,
                    new Dictionary<string, string> { },
                    new List<EmailAttachment>(),
                    new Dictionary<string, string> { }
                ));
            }
        }
    }
}
