using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.Review;

namespace WorkshopHub.Domain.Commands.Reviews.CreateReview
{
    public sealed class CreateReviewCommandHandler : CommandHandlerBase, IRequestHandler<CreateReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;

        public CreateReviewCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IReviewRepository reviewRepository
        ) : base(bus, unitOfWork, notifications) 
        {
            _reviewRepository = reviewRepository;
        }

        public async Task Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var review = new Entities.Review(
                request.ReviewId,
                request.UserId,
                request.WorkshopId,
                request.Rating,
                request.Comment
            );

            _reviewRepository.Add(review);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new ReviewCreatedEvent(review.Id));
            }
        }
    }
}
