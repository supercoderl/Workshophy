using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Domain.Commands.Users.CreateUser
{
    public sealed class CreateUserCommand : CommandBase
    {
        private static readonly CreateUserCommandValidation s_validation = new();

        public Guid UserId { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Password { get; }
        public string PhoneNumber { get; }
        public UserRole UserRole { get; }
        public UserStatus Status { get; }

        public CreateUserCommand(
            Guid userId,
            string email,
            string firstName,
            string lastName,
            string password,
            string phoneNumber,
            UserRole userRole,
            UserStatus status
        ) : base(userId)
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            PhoneNumber = phoneNumber;
            UserRole = userRole;
            Status = status;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
