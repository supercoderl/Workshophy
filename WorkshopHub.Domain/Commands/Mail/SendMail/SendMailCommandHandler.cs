using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Domain.Settings;
using WorkshopHub.Shared.Events;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Helpers;
namespace WorkshopHub.Domain.Commands.Mail.SendMail
{
    public sealed class SendMailCommandHandler : CommandHandlerBase, IRequestHandler<SendMailCommand>
    {
        private readonly SmtpSettings _smtpSetting;

        public SendMailCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IOptions<SmtpSettings> smtpSettings
        ) : base(bus, unitOfWork, notifications)
        {
            _smtpSetting = smtpSettings.Value;
        }

        public async Task Handle(SendMailCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            using var client = new SmtpClient(_smtpSetting.Server)
            {
                Port = _smtpSetting.Port,
                Credentials = new NetworkCredential(_smtpSetting.Username, _smtpSetting.Password),
                EnableSsl = _smtpSetting.EnableSsl
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSetting.FromEmail, _smtpSetting.FromName),
                Subject = request.Subject,
                Body = TemplateHelpers.ProcessTemplate(request.HtmlBody, request.TemplateData),
                IsBodyHtml = request.IsHtml
            };

            mailMessage.To.Add(request.To);

            // Add attachments if any
            foreach (var attachment in request.Attachments)
            {
                using var ms = new MemoryStream(attachment.Content);
                mailMessage.Attachments.Add(new Attachment(ms, attachment.FileName, attachment.ContentType));
            }

            // Add custom headers if any
            foreach (var header in request.Headers)
            {
                mailMessage.Headers.Add(header.Key, header.Value);
            }

            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"Sending mail failed. Error: {ex}",
                    ErrorCodes.CommitFailed
                ));
                return;
            }
        }
    }
}
