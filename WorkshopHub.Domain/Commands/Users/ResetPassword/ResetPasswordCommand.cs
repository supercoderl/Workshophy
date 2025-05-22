using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Users.ResetPassword
{
    public sealed class ResetPasswordCommand : CommandBase, IRequest
    {
        private static readonly ResetPasswordCommandValidation s_validation = new();

        public string OTP { get; }
        public string NewPassword { get; }

        public ResetPasswordCommand(
            string otp,
            string newPassword
        ) : base(Guid.NewGuid())
        {
            OTP = otp;
            NewPassword = newPassword;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
