using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Categories.UpdateCategory
{
    public sealed class UpdateCategoryCommandValidation : AbstractValidator<UpdateCategoryCommand>  
    {
        public UpdateCategoryCommandValidation()
        {
            RuleForId();
            RuleForName();
        }

        public void RuleForId()
        {
            RuleFor(cmd => cmd.CategoryId).NotEmpty().WithErrorCode(DomainErrorCodes.Category.EmptyId).WithMessage("Id may not be empty.");
        }

        public void RuleForName()
        {
            RuleFor(cmd => cmd.Name).NotEmpty().WithErrorCode(DomainErrorCodes.Category.EmptyName).WithMessage("Name may not be empty.");
        }
    }
}
