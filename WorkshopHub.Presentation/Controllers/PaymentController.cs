using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.Services;
using WorkshopHub.Domain.Notifications;

namespace WorkshopHub.Presentation.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class PaymentController : ApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ITemplateService _templateService;

        public PaymentController(
            INotificationHandler<DomainNotification> notifications,
            IPaymentService paymentService,
            ITemplateService templateService
        ) : base(notifications)
        {
            _paymentService = paymentService;
            _templateService = templateService;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferWebhook(WebhookType body)
        {
            await _paymentService.HandleResponseAsync(body, await _templateService.GetHtmlBodyFromTemplateAsync("TicketConfirmation"));
            return Response(new
            {
                IsSuccess = true
            });
        }
    }
}
