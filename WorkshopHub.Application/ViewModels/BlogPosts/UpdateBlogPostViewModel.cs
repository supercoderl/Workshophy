using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels.BlogPosts
{
    public sealed record UpdateBlogPostViewModel
    (
        Guid BlogPostId,
        string Title,
        string Content,
        Guid UserId
    );
}
