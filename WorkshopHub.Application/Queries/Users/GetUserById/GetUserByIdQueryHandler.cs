using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Users;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;

namespace WorkshopHub.Application.Queries.Users.GetUserById
{
    public sealed class GetUserByIdQueryHandler :
        IRequestHandler<GetUserByIdQuery, UserViewModel?>
    {
        private readonly IMediatorHandler _bus;
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMediatorHandler bus)
        {
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task<UserViewModel?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        nameof(GetUserByIdQuery),
                        $"User with id {request.Id} could not be found",
                        ErrorCodes.ObjectNotFound));
                return null;
            }

            return UserViewModel.FromUser(user);
        }
    }
}
