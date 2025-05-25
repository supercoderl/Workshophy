using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.Interfaces
{
    public interface IPaymentService
    {
        public Task HandleResponseAsync(WebhookType body, string htmlBody);
    }
}
