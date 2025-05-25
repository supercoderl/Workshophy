using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Mails;

namespace WorkshopHub.Application.Interfaces
{
    public interface IMailService
    {
        public Task SendMailAsync(SendMailViewModel viewModel);
    }
}
