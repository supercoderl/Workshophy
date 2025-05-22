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

namespace WorkshopHub.Domain.Commands.Users.DeleteUser
{
    public sealed class DeleteUserCommandHandler : CommandHandlerBase,
        IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IUserRepository userRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
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

            _userRepository.Remove(user);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new UserDeletedEvent(request.UserId));
            }
        }
    }
}
