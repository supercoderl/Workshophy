using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Models;

namespace WorkshopHub.Domain.Commands.Mail.SendMail
{
    public sealed class SendMailCommand : CommandBase, IRequest
    {
        private static readonly SendMailCommandValidation s_validation = new();

        public string To { get; }
        public string Subject { get; }
        public string HtmlBody { get; }
        public bool IsHtml { get; }
        public Dictionary<string, string> TemplateData { get; }
        public List<EmailAttachment> Attachments { get; }
        public Dictionary<string, string> Headers { get; }

        public SendMailCommand(
            string to,
            string subject,
            string htmlBody,
            bool isHtml,
            Dictionary<string, string> templateData,
            List<EmailAttachment> attachments,
            Dictionary<string, string> headers
        ) : base(Guid.NewGuid())
        {
            To = to;
            Subject = subject;
            HtmlBody = htmlBody;
            IsHtml = isHtml;
            TemplateData = templateData;
            Attachments = attachments;
            Headers = headers;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
