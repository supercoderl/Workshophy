using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Categories.HandleFavourite
{
    public sealed class HandleFavouriteCommand : CommandBase, IRequest
    {
        private static readonly HandleFavouriteCommandValidation s_validation = new();

        public ICollection<Guid> CategoryIds { get; }

        public HandleFavouriteCommand(
            ICollection<Guid> categoryIds
        ) : base(Guid.NewGuid())
        {
            CategoryIds = categoryIds;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
