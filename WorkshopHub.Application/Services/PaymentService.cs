using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Domain.Commands.Payments.PayOS.HandleRespose;
using WorkshopHub.Domain.Interfaces;

namespace WorkshopHub.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMediatorHandler _bus;

        public PaymentService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task HandleResponseAsync(WebhookType body, string htmlBody)
        {
            await _bus.SendCommandAsync(new HandleResponseCommand(body, htmlBody));
        }
    }
}
