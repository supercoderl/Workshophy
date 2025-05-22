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

        public DeleteReviewCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IReviewRepository reviewRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var review = await _reviewRepository.GetByIdAsync(request.ReviewId);

            if(review == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any review with id: {request.ReviewId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            _reviewRepository.Remove(review);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ReviewDeletedEvent(request.ReviewId));
            }
        }
    }
}
