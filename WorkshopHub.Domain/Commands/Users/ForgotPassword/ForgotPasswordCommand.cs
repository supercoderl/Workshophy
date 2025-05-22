using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Models;

namespace WorkshopHub.Domain.Commands.Users.ForgotPassword
{
    public sealed class ForgotPasswordCommand : CommandBase
    {
        private static readonly ForgotPasswordCommandValidation s_validation = new();

        public string Email { get; }
        public string HtmlBody { get; }

        public ForgotPasswordCommand(string email, string htmlBody) : base(Guid.NewGuid())
        {
            Email = email;
            HtmlBody = htmlBody;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
