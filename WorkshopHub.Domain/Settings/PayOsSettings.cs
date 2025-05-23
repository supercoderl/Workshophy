using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Settings
{
    public sealed class PayOsSettings
    {
        public string BaseUrl { get; set; } = null!;
        public string ReturnUrl { get; set; } = null!;
        public string CancelUrl { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string ClientID { get; set; } = null!;
        public string ChecksumKey { get; set; } = null!;
    }
}
