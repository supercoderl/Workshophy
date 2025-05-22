using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Badges.UpdateBadge
{
    public sealed class UpdateBadgeCommand : CommandBase, IRequest
    {
        private static readonly UpdateBadgeCommandValidation s_validation = new();

        public Guid BadgeId { get; }
        public string Name { get; }
        public string? Description { get; }
        public string Area { get; }
        public string ImageUrl { get; }

        public UpdateBadgeCommand(
            Guid badgeId,
            string name,
            string? description,
            string area,
            string imageUrl
        ) : base(Guid.NewGuid())
        {
            BadgeId = badgeId;
            Name = name;
            Description = description;
            Area = area;
            ImageUrl = imageUrl;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
