using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.ViewModels.Mails;
using WorkshopHub.Domain.Commands.Mail.SendMail;
using WorkshopHub.Domain.Interfaces;

namespace WorkshopHub.Application.Services
{
    public class MailService : IMailService
    {
        private readonly IMediatorHandler _bus;

        public MailService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task SendMailAsync(SendMailViewModel viewModel)
        {
            await _bus.SendCommandAsync(new SendMailCommand(
                viewModel.To,
                viewModel.Subject,
                viewModel.HtmlBody,
                viewModel.IsHtml,
                viewModel.TemplateData,
                viewModel.Attachments,
                viewModel.Headers
            ));
        }
    }
}
