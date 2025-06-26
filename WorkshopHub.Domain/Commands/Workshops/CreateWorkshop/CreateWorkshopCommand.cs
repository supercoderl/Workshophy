using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Domain.Commands.Workshops.CreateWorkshop
{
    public sealed class CreateWorkshopCommand : CommandBase, IRequest
    {
        private static readonly CreateWorkshopCommandValidation s_validation = new();

        public Guid WorkshopId { get; }
        public Guid OrganizerId { get; }
        public string Title { get; }
        public string? Description { get; }
        public Guid CategoryId { get; }
        public string Location { get; }
        public string? IntroVideoUrl { get; }
        public decimal Price { get; }

        public CreateWorkshopCommand(
            Guid workshopId,
            Guid organizerId,
            string title,
            string? description,
            Guid categoryId,
            string location,
            string? introVideoUrl,
            decimal price
        ) : base(Guid.NewGuid())
        {
            WorkshopId = workshopId;
            OrganizerId = organizerId;
            Title = title;
            Description = description;
            CategoryId = categoryId;
            Location = location;
            IntroVideoUrl = introVideoUrl;
            Price = price;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
