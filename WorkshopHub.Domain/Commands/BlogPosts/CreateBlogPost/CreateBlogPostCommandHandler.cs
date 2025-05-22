using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.BlogPost;

namespace WorkshopHub.Domain.Commands.BlogPosts.CreateBlogPost
{
    public sealed class CreateBlogPostCommandHandler : CommandHandlerBase, IRequestHandler<CreateBlogPostCommand>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public CreateBlogPostCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IBlogPostRepository blogPostRepository
        ) : base(bus, unitOfWork, notifications ) 
        {
            _blogPostRepository = blogPostRepository;
        }

        public async Task Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var blogPost = new Entities.BlogPost(
                request.BlogPostId,
                request.Title,
                request.Content,
                request.UserId
            );

            _blogPostRepository.Add( blogPost );

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new BlogPostCreatedEvent(blogPost.Id));
            }
        }
    }
}
