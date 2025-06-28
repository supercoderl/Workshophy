using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Domain.Commands.Workshops.UpdateWorkshop
{
    public sealed class UpdateWorkshopCommand : CommandBase, IRequest
    {
        private static readonly UpdateWorkshopCommandValidation s_validation = new();

        public Guid WorkshopId { get; }
        public Guid OrganizerId { get; }
        public string Title { get; }
        public string? Description { get; }
        public Guid CategoryId { get; }
        public string Location { get; }
        public string? IntroVideoUrl { get; }
        public decimal Price { get; }
        public WorkshopStatus Status { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public ScheduleStatus ScheduleStatus { get; }

        public UpdateWorkshopCommand(
            Guid workshopId,
            Guid organizerId,
            string title,
            string? description,
            Guid categoryId,
            string location,
            string? introVideoUrl,
            decimal price,
            WorkshopStatus status,
            DateTime startTime,
            DateTime endTime,
            ScheduleStatus scheduleStatus
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
            Status = status;
            StartTime = startTime;
            EndTime = endTime;
            ScheduleStatus = scheduleStatus;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
