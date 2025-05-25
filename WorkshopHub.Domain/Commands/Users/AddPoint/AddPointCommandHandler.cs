using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.User;

namespace WorkshopHub.Domain.Commands.Users.AddPoint
{
    public sealed class AddPointCommandHandler : CommandHandlerBase, IRequestHandler<AddPointCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IActivityPointRuleRepository _activityPointRuleRepository;

        public AddPointCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IUserRepository userRepository,
            IActivityPointRuleRepository activityPointRuleRepository
        ) : base(bus, unitOfWork, notifications ) 
        {
            _userRepository = userRepository;
            _activityPointRuleRepository = activityPointRuleRepository;
        }

        public async Task Handle(AddPointCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var user = await _userRepository.GetByIdAsync(request.UserId);

            if(user == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any user with id: {request.UserId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            var activityPointRule = await _activityPointRuleRepository.GetByType(request.Type);

            if(activityPointRule == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any rule to add point for user.",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            int totalPoint = user.AchievementPoint + activityPointRule.ActivityPoint;

            user.SetAchievementPoint(totalPoint);

            _userRepository.Update(user);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new UserUpdatedEvent(request.UserId));
            }
        }
    }
}
