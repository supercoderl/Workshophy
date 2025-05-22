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
using WorkshopHub.Shared.Events.BlogPost;

namespace WorkshopHub.Domain.Commands.BlogPosts.DeleteBlogPost
{
    public sealed class DeleteBlogPostCommandHandler : CommandHandlerBase, IRequestHandler<DeleteBlogPostCommand>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public DeleteBlogPostCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IBlogPostRepository blogPostRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _blogPostRepository = blogPostRepository;
        }

        public async Task Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var blogPost = await _blogPostRepository.GetByIdAsync(request.BlogPostId);

            if(blogPost == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any post with id: {request.BlogPostId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            _blogPostRepository.Remove(blogPost);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new BlogPostDeletedEvent(request.BlogPostId));
            }
        }
    }
}
