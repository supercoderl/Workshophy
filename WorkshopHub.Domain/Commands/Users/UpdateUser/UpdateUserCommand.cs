using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Domain.Commands.Users.UpdateUser
{
    public sealed class UpdateUserCommand : CommandBase
    {
        private static readonly UpdateUserCommandValidation s_validation = new();

        public Guid UserId { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string PhoneNumber { get; }
        public UserRole Role { get; }
        public UserStatus Status { get; }

        public UpdateUserCommand(
            Guid userId,
            string email,
            string firstName,
            string lastName,
            string phoneNumber,
            UserRole role,
            UserStatus status
        ) : base(userId)
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Role = role;
            Status = status;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
