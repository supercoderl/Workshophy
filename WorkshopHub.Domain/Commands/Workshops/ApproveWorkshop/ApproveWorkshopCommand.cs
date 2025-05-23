using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Workshops.ApproveWorkshop
{
    public sealed class ApproveWorkshopCommand : CommandBase, IRequest
    {
        private static readonly ApproveWorkshopCommandValidation s_validation = new();

        public Guid WorkshopId { get; }
        public bool IsAccept {  get; }

        public ApproveWorkshopCommand(
            Guid workshopId,
            bool isAccept
        ) : base(Guid.NewGuid())
        {
            WorkshopId = workshopId;
            IsAccept = isAccept;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
