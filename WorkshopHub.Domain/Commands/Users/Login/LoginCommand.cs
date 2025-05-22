using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Users.Login
{
    public sealed class LoginCommand : CommandBase,
        IRequest<object>
    {
        private static readonly LoginCommandValidation s_validation = new();

        public string Email { get; set; }
        public string Password { get; set; }


        public LoginCommand(
            string email,
            string password) : base(Guid.NewGuid())
        {
            Email = email;
            Password = password;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
