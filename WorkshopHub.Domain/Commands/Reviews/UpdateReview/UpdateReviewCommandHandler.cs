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

namespace WorkshopHub.Domain.Commands.Reviews.UpdateReview
{
    public sealed class UpdateReviewCommandHandler : CommandHandlerBase, IRequestHandler<UpdateReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUser _user;

        public UpdateReviewCommandHandler(
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

        public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var review = await _reviewRepository.GetByIdAsync(request.ReviewId);

            if(review == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any review with id: {request.ReviewId}",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            if (_user.GetUserId() != review.UserId)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"You cannot edit other people's review.",
                    ErrorCodes.NotAllowChange
                ));
                return;
            }

            review.SetUserId(request.UserId);   
            review.SetWorkshopId(request.WorkshopId);
            review.SetRating(request.Rating);
            review.SetComment(request.Comment);

            _reviewRepository.Update(review);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ReviewUpdatedEvent(request.ReviewId));
            }
        }
    }
}
