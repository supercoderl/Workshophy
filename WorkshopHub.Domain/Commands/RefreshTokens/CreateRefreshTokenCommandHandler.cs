using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.RefreshToken;

namespace WorkshopHub.Domain.Commands.RefreshTokens
{
    public sealed class CreateRefreshTokenCommandHandler : CommandHandlerBase, IRequestHandler<CreateRefreshTokenCommand>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public CreateRefreshTokenCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IRefreshTokenRepository refreshTokenRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var refreshToken = new Entities.RefreshToken(
                request.RefreshTokenId,
                request.UserId,
                request.Token,
                request.ExpiryDate
            );

            _refreshTokenRepository.Add( refreshToken );    

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new RefreshTokenCreatedEvent(refreshToken.Id));
            }
        }
    }
}
