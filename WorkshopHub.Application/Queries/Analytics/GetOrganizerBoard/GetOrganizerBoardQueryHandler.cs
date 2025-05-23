using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Queries.Users.GetUserById;
using WorkshopHub.Application.ViewModels.Analytics;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;

namespace WorkshopHub.Application.Queries.Analytics.GetOrganizerBoard
{
    public sealed class GetOrganizerBoardQueryHandler : IRequestHandler<GetOrganizerBoardQuery, OrganizerBoardViewModel>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUser _user;

        public GetOrganizerBoardQueryHandler(
           IReviewRepository reviewRepository,
           IUser user
        )
        {
            _reviewRepository = reviewRepository;
            _user = user;
        }

        public async Task<OrganizerBoardViewModel> Handle(GetOrganizerBoardQuery request, CancellationToken cancellationToken)
        {
            var currentReview = await _reviewRepository.GetCurrentReviewByUser(_user.GetUserId());
            var bestRatingWorkshop = await _reviewRepository.GetBestRatingWorkshop(_user.GetUserId());

            return OrganizerBoardViewModel.FromOrganizerBoard(currentReview, bestRatingWorkshop);
        }
    }
}
