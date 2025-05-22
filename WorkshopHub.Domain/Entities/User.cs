using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Domain.Entities
{
    public class User : Entity
    {
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Password { get; private set; }
        public UserRole Role { get; private set; }
        public UserStatus Status { get; private set; }
        public DateTimeOffset? LastLoggedinDate { get; private set; }

        public string FullName => $"{FirstName}, {LastName}";

        [InverseProperty("User")]
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        [InverseProperty("User")]
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        [InverseProperty("Author")]
        public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();

        [InverseProperty("User")]
        public virtual ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();

        [InverseProperty("User")]
        public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();

        [InverseProperty("User")]
        public virtual ICollection<UserInterest> UserInterests { get; set; } = new List<UserInterest>();

        [InverseProperty("User")]
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        [InverseProperty("User")]
        public virtual ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();

        [InverseProperty("User")]
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public User(
            Guid id,
            string email,
            string firstName,
            string lastName,
            string password,
            UserRole role,
            UserStatus status
        ) : base(id)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Role = role;
            Status = status;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetFirstName(string firstName)
        {
            FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            LastName = lastName;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void SetRole(UserRole role)
        {
            Role = role;
        }

        public void SetLastLoggedinDate(DateTimeOffset lastLoggedinDate)
        {
            LastLoggedinDate = lastLoggedinDate;
        }

        public void SetStatus(UserStatus status)
        {
            Status = status;
        }
    }
}
