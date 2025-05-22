using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Badges.DeleteBadge
{
    public sealed class DeleteBadgeCommand : CommandBase, IRequest
    {
        private static readonly DeleteBadgeCommandValidation s_validation = new();

        public Guid BadgeId { get; }

        public DeleteBadgeCommand(Guid badgeId) : base(Guid.NewGuid())
        {
            BadgeId = badgeId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
