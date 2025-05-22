using MediatR;
using System;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Application.ViewModels.Reviews;

namespace WorkshopHub.Application.Queries.Reviews.GetReviewById
{
    public sealed class GetReviewByIdQueryHandler :
            IRequestHandler<GetReviewByIdQuery, ReviewViewModel?>
    {
        private readonly IMediatorHandler _bus;
        private readonly IReviewRepository _reviewRepository;

        public GetReviewByIdQueryHandler(IReviewRepository reviewRepository, IMediatorHandler bus)
        {
            _reviewRepository = reviewRepository;
            _bus = bus;
        }

        public async Task<ReviewViewModel?> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetByIdAsync(request.Id);

            if (review is null)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        nameof(GetReviewByIdQuery),
                        $"Review with id {request.Id} could not be found",
                        ErrorCodes.ObjectNotFound));
                return null;
            }

            return ReviewViewModel.FromReview(review);
        }
    }
}
