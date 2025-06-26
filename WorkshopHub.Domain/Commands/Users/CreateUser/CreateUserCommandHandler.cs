using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Enums;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.User;
using BC = BCrypt.Net.BCrypt;

namespace WorkshopHub.Domain.Commands.Users.CreateUser
{
    public sealed class CreateUserCommandHandler : CommandHandlerBase,
        IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IUserRepository userRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request))
            {
                return;
            }

            var existingUser = await _userRepository.GetByIdAsync(request.UserId);

            if (existingUser is not null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is already a user with Id {request.UserId}",
                        DomainErrorCodes.User.AlreadyExists));
                return;
            }

            existingUser = await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser is not null)
            {
                await NotifyAsync(
                    new DomainNotification(
                        request.MessageType,
                        $"There is already a user with email {request.Email}",
                        DomainErrorCodes.User.AlreadyExists));
                return;
            }

            var passwordHash = BC.HashPassword(request.Password);

            var user = new User(
                request.UserId,
                request.Email,
                request.FirstName,
                request.LastName,
                passwordHash,
                request.PhoneNumber,
                0,
                request.AccountBank,
                request.UserRole,
                request.Status
            );

            _userRepository.Add(user);

            if (await CommitAsync())
            {
                await Bus.RaiseEventAsync(new UserCreatedEvent(user.Id));
            }
        }
    }
}
