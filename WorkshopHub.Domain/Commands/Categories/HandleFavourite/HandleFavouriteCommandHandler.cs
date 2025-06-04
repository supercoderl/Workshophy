using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;

namespace WorkshopHub.Domain.Commands.Categories.HandleFavourite
{
    public sealed class HandleFavouriteCommandHandler : CommandHandlerBase, IRequestHandler<HandleFavouriteCommand>
    {
        private readonly IUserInterestRepository _userInterestRepository;
        private readonly IUser _user;

        public HandleFavouriteCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IUserInterestRepository userInterestRepository,
            IUser user
        ) : base(bus, unitOfWork, notifications)
        {
            _userInterestRepository = userInterestRepository;
            _user = user;
        }

        public async Task Handle(HandleFavouriteCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var userId = _user.GetUserId();

            if(userId == Guid.Empty)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"You have not signed in.",
                    ErrorCodes.InsufficientPermissions
                ));
                return;
            }

            var inputCategoryIds = request.CategoryIds.ToHashSet();

            var currentCategoryIds = await _userInterestRepository.GetAllNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.CategoryId)
                .ToListAsync(cancellationToken);

            var toKeep = currentCategoryIds.Intersect(inputCategoryIds).ToList();      // giữ lại
            var toDelete = currentCategoryIds.Except(inputCategoryIds).ToList();       // xóa
            var toAdd = inputCategoryIds.Except(currentCategoryIds).ToList();          // thêm

            if (toDelete.Any())
            {
                var deleteEntities = await _userInterestRepository.GetByCollection(toDelete, userId);

                _userInterestRepository.RemoveRange(deleteEntities, true);
            }

            // Thêm
            foreach (var catId in toAdd)
            {
                var userInterest = new Entities.UserInterest(Guid.NewGuid(), userId, catId);
                _userInterestRepository.Add(userInterest);
            }

            if(await CommitAsync())
            {

            }
        }
    }
}
