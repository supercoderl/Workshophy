using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.User;

namespace WorkshopHub.Domain.Commands.Users.UpdateUser
{
    public sealed class UpdateUserCommandHandler : CommandHandlerBase,
        IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IUserRepository userRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is no user with Id {request.UserId}",
                        ErrorCodes.ObjectNotFound));
                return;
            }

            if (request.Email != user.Email)
            {
                var existingUser = await _userRepository.GetByEmailAsync(request.Email);

                if (existingUser is not null)
                {
                    await NotifyAsync(
                        new DomainNotification(
                            request.MessageType,
                            $"There is already a user with email {request.Email}",
                            DomainErrorCodes.User.AlreadyExists));
                    return;
                }
            }

            user.SetEmail(request.Email);
            user.SetFirstName(request.FirstName);
            user.SetLastName(request.LastName);
            user.SetStatus(request.Status);

            _userRepository.Update(user);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new UserUpdatedEvent(user.Id));
            }
        }
    }
}
