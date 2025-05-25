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
using WorkshopHub.Shared.Events.Review;

namespace WorkshopHub.Domain.Commands.Reviews.DeleteReview
{
    public sealed class DeleteReviewCommandHandler : CommandHandlerBase, IRequestHandler<DeleteReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUser _user;

        public DeleteReviewCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IReviewRepository reviewRepository,
            IUser user
        ) : base(bus, unitOfWork, notifications)
        {
            _reviewRepository = reviewRepository;
            _user = user;
        }

        public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var review = await _reviewRepository.GetByIdAsync(request.ReviewId, r => r.Workshop!);

            if(review == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any review with id: {request.ReviewId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            if(_user.GetUserId() != review.UserId)
            {
                if ((_user.GetUserRole() != Enums.UserRole.Admin) &&
                   (_user.GetUserRole() == Enums.UserRole.Organizer && review.Workshop != null && review.Workshop.OrganizerId != _user.GetUserId())
                )
                {
                    await NotifyAsync(new DomainNotification(
                        request.MessageType,
                        $"You have not permission to delete this review.",
                        ErrorCodes.NotHavePermission
                    ));
                    return;
                }
            }

            _reviewRepository.Remove(review);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ReviewDeletedEvent(request.ReviewId));
            }
        }
    }
}
