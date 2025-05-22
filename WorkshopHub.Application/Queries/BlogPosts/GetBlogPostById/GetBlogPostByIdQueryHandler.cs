using MediatR;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Application.ViewModels.BlogPosts;

namespace WorkshopHub.Application.Queries.BlogPosts.GetBlogPostById
{
    public sealed class GetBlogPostByIdQueryHandler :
                IRequestHandler<GetBlogPostByIdQuery, BlogPostViewModel?>
    {
        private readonly IMediatorHandler _bus;
        private readonly IBlogPostRepository _blogPostRepository;

        public GetBlogPostByIdQueryHandler(IBlogPostRepository blogPostRepository, IMediatorHandler bus)
        {
            _blogPostRepository = blogPostRepository;
            _bus = bus;
        }

        public async Task<BlogPostViewModel?> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(request.Id);

            if (blogPost is null)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        nameof(GetBlogPostByIdQuery),
                        $"BlogPost with id {request.Id} could not be found",
                        ErrorCodes.ObjectNotFound));
                return null;
            }

            return BlogPostViewModel.FromBlogPost(blogPost);
        }
    }
}
