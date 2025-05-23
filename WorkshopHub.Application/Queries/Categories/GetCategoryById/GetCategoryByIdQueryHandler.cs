using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Queries.Tickets.GetTicketById;
using WorkshopHub.Application.ViewModels.Tickets;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Application.ViewModels.Categories;

namespace WorkshopHub.Application.Queries.Categories.GetCategoryById
{
    public sealed class GetCategoryByIdQueryHandler :
                IRequestHandler<GetCategoryByIdQuery, CategoryViewModel?>
    {
        private readonly IMediatorHandler _bus;
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMediatorHandler bus)
        {
            _categoryRepository = categoryRepository;
            _bus = bus;
        }

        public async Task<CategoryViewModel?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);

            if (category is null)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        nameof(GetTicketByIdQuery),
                        $"Category with id {request.Id} could not be found",
                        ErrorCodes.ObjectNotFound));
                return null;
            }

            return CategoryViewModel.FromCategory(category);
        }
    }
}
