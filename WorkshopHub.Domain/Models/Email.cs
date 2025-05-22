using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Models
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; } = true;
        public List<EmailAttachment> Attachments { get; set; } = new List<EmailAttachment>();
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }

    public class EmailAttachment
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
