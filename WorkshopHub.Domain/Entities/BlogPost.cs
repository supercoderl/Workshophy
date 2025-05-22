using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class BlogPost : Entity
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("BlogPosts")]
        public virtual User? Author { get; set; }

        public BlogPost(
            Guid id,
            string title,
            string content,
            Guid userId
        ) : base(id)
        {
            Title = title;
            Content = content;
            UserId = userId;
        }

        public void SetTitle( string title ) {  Title = title; }
        public void SetContent( string content ) { Content = content; }
        public void SetUserId( Guid userId ) { UserId = userId; }
    }
}
