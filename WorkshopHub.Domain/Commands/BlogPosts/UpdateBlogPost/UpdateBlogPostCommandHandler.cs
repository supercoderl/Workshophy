using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Shared.Events.BlogPost;

namespace WorkshopHub.Domain.Commands.BlogPosts.UpdateBlogPost
{
    public sealed class UpdateBlogPostCommandHandler : CommandHandlerBase, IRequestHandler<UpdateBlogPostCommand>   
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public UpdateBlogPostCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IBlogPostRepository blogPostRepository
        ) : base(bus, unitOfWork, notifications )
        {
            _blogPostRepository = blogPostRepository;
        }

        public async Task Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var blogPost = await _blogPostRepository.GetByIdAsync(request.BlogPostId);

            if (blogPost == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any post with id: {request.BlogPostId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            blogPost.SetTitle(request.Title);
            blogPost.SetContent(request.Content);
            blogPost.SetUserId(request.UserId);

            _blogPostRepository.Update(blogPost);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new BlogPostUpdatedEvent(request.BlogPostId));
            }
        }
    }
}
