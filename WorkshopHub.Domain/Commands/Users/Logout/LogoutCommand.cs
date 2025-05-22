using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Users.Logout
{
    public sealed class LogoutCommand : CommandBase, IRequest
    {
        private static readonly LogoutCommandValidation s_validation = new();

        public string Token { get; }

        public LogoutCommand(string token) : base(Guid.NewGuid())
        {
            Token = token;  
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
