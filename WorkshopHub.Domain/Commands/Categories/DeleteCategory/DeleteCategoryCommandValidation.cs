using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Categories.DeleteCategory
{
    public sealed class DeleteCategoryCommandValidation : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidation()
        {
            RuleForId();
        }

        public void RuleForId()
        {
            RuleFor(cmd => cmd.CategoryId).NotEmpty().WithErrorCode(DomainErrorCodes.Category.EmptyId).WithMessage("Id may not be empty.");
        }
    }
}
