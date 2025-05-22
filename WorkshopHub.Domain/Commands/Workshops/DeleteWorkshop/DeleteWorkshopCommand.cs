using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Workshops.DeleteWorkshop
{
    public sealed class DeleteWorkshopCommand : CommandBase, IRequest
    {
        private static readonly DeleteWorkshopCommandValidation s_validation = new();

        public Guid WorkshopId { get; }

        public DeleteWorkshopCommand(Guid workshopId) : base(Guid.NewGuid())
        {
            WorkshopId = workshopId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
