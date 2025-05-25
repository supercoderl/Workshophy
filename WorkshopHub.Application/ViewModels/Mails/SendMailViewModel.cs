using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Models;

namespace WorkshopHub.Application.ViewModels.Mails
{
    public sealed record SendMailViewModel
    (
        string To,
        string Subject,
        string HtmlBody,
        bool IsHtml,
        Dictionary<string, string> TemplateData,
        List<EmailAttachment> Attachments,
        Dictionary<string, string> Headers
    );
}
