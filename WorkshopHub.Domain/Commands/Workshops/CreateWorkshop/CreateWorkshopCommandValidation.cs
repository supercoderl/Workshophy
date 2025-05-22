using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Workshops.CreateWorkshop
{
    public sealed class CreateWorkshopCommandValidation : AbstractValidator<CreateWorkshopCommand>
    {
        public CreateWorkshopCommandValidation()
        {
            RuleForOrganizerId();
            RuleForTitle();
            RuleForCategoryId();
            RuleForLocation();
        }

        public void RuleForOrganizerId()
        {
            RuleFor(cmd => cmd.OrganizerId).NotEmpty().WithErrorCode(DomainErrorCodes.Workshop.EmptyOrganizerId).WithMessage("Organizer id may not be empty.");
        }

        public void RuleForTitle()
        {
            RuleFor(cmd => cmd.Title).NotEmpty().WithErrorCode(DomainErrorCodes.Workshop.EmptyTitle).WithMessage("Title may not be empty.");
        }

        public void RuleForCategoryId()
        {
            RuleFor(cmd => cmd.CategoryId).NotEmpty().WithErrorCode(DomainErrorCodes.Workshop.EmptyCategoryId).WithMessage("Category id may not be empty.");
        }

        public void RuleForLocation()
        {
            RuleFor(cmd => cmd.Location).NotEmpty().WithErrorCode(DomainErrorCodes.Workshop.EmptyLocation).WithMessage("Location may not be empty.");
        }
    }
}
