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
using WorkshopHub.Shared.Events.Category;

namespace WorkshopHub.Domain.Commands.Categories.UpdateCategory
{
    public sealed class UpdateCategoryCommandHandler : CommandHandlerBase, IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICategoryRepository categoryRepository
        ) : base( bus, unitOfWork, notifications )
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if(category == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any category with id: {request.CategoryId}.",
                    ErrorCodes.ObjectNotFound
                ));

                return;
            }

            category.SetName(request.Name);
            category.SetDescription(request.Description);

            _categoryRepository.Update(category);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CategoryUpdatedEvent(request.CategoryId));
            }
        }
    }
}
