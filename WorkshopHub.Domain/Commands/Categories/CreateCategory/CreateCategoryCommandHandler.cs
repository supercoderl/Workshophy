using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.Category;

namespace WorkshopHub.Domain.Commands.Categories.CreateCategory
{
    public sealed class CreateCategoryCommandHandler : CommandHandlerBase, IRequestHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ICategoryRepository categoryRepository
        ) : base(bus, unitOfWork, notifications) 
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var category = new Entities.Category(
                request.CategoryId,
                request.Name,
                request.Description
            );

            _categoryRepository.Add( category );    

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new CategoryCreatedEvent(category.Id));
            }
        }
    }
}
