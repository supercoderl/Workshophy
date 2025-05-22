using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Mail.SendMail
{
    public sealed class SendMailCommandValidation : AbstractValidator<SendMailCommand>
    {
        public SendMailCommandValidation()
        {
            
        }
    }
}
