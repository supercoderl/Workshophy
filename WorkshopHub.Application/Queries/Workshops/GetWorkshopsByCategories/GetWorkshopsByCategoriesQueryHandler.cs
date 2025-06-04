using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Workshops;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;

namespace WorkshopHub.Application.Queries.Workshops.GetWorkshopsByCategories
{
    public sealed class GetWorkshopsByCategoriesQueryHandler : IRequestHandler<GetWorkshopsByCategoriesQuery, PagedResult<WorkshopViewModel>>
    {
        private readonly IWorkshopRepository _workshopRepository;
        private readonly IUserInterestRepository _userInterestRepository;
        private readonly IUser _user;

        public GetWorkshopsByCategoriesQueryHandler(
            IWorkshopRepository workshopRepository,
            IUserInterestRepository userInterestRepository,
            IUser user
        )
        {
            _workshopRepository = workshopRepository;
            _userInterestRepository = userInterestRepository;
            _user = user;
        }

        public async Task<PagedResult<WorkshopViewModel>> Handle(GetWorkshopsByCategoriesQuery request, CancellationToken cancellationToken)
        {
            var userId = _user.GetUserId();

            if (userId == Guid.Empty)
            {
                return new PagedResult<WorkshopViewModel>(0, [], request.Query.Page, request.Query.PageSize);
            }

            var favouriteCategoryIds = await _userInterestRepository.GetAllNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.CategoryId)
                .ToListAsync(cancellationToken);

            var workshopsQuery = _workshopRepository.GetByCategories(favouriteCategoryIds);

            var totalCount = await workshopsQuery.CountAsync(cancellationToken);

            var workshops = await workshopsQuery
                .Skip((request.Query.Page - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize)
                .Select(workshop => WorkshopViewModel.FromWorkshop(workshop))
                .ToListAsync(cancellationToken);

            return new PagedResult<WorkshopViewModel>(
                totalCount, workshops, request.Query.Page, request.Query.PageSize);
        }
    }
}
