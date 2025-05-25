using FluentValidation;
using MediatR;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Payments.PayOS.HandleRespose
{
    public sealed class HandleResponseCommand : CommandBase, IRequest
    {
        private static readonly HandleResponseCommandValidation s_validation = new();

        public WebhookType Body { get; }
        public string HtmlBody { get; }

        public HandleResponseCommand(WebhookType body, string htmlBody) : base(Guid.NewGuid())
        {
            Body = body;
            HtmlBody = htmlBody;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
