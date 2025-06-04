using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Users.LoginGoogle
{
    public sealed class LoginGoogleCommand : CommandBase, IRequest<object>
    {
        private static readonly LoginGoogleCommandValidation s_validation = new();

        public string Code { get; }
        public string RedirectUri { get; }

        public LoginGoogleCommand(
            string code,
            string redirectUri
        ) : base(Guid.NewGuid())
        {
            Code = code;
            RedirectUri = redirectUri;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
