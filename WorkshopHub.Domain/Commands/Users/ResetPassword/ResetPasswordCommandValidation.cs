using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Users.ResetPassword
{
    public sealed class ResetPasswordCommandValidation : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidation()
        {
            
        }

        public void RuleForOTP()
        {

        }

        public void RuleForPassword()
        {

        }
    }
}
