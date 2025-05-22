using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Users.RefreshToken
{
    public sealed class RefreshTokenCommandValidation : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidation()
        {
            
        }

        public void RuleForToken()
        {
            RuleFor(cmd => cmd.Token).NotEmpty().WithErrorCode(DomainErrorCodes.RefreshToken.EmptyToken).WithMessage("Token may not be empty.");
        }
    }
}
