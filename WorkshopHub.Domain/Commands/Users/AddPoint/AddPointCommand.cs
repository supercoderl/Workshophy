using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Users.AddPoint
{
    public sealed class AddPointCommand : CommandBase, IRequest
    {
        private static readonly AddPointCommandValidation s_validation = new();

        public Guid UserId { get; }
        public string Type { get; }

        public AddPointCommand(Guid userId, string type) : base(Guid.NewGuid())
        {
            UserId = userId;
            Type = type;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
