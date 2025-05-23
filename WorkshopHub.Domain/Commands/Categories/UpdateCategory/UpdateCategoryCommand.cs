using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Categories.UpdateCategory
{
    public sealed class UpdateCategoryCommand : CommandBase, IRequest
    {
        private static readonly UpdateCategoryCommandValidation s_validation = new();

        public Guid CategoryId { get; }
        public string Name { get; }
        public string? Description { get; }

        public UpdateCategoryCommand(
            Guid categoryId,
            string name,
            string? description
        ) : base(Guid.NewGuid())
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
