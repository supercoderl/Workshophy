using MediatR;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Helpers;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;

namespace WorkshopHub.Domain.Commands.Payments.PayOS.CreateOrder
{
    public sealed class CreatePayOSOrderCommandHandler : CommandHandlerBase, IRequestHandler<CreatePayOSOrderCommand, string>
    {
        private readonly HttpClient _httpClient;
        private readonly Net.payOS.PayOS _payOS;

        public CreatePayOSOrderCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IHttpClientFactory httpClientFactory,
            Net.payOS.PayOS payOS
        ) : base(bus , unitOfWork, notifications ) 
        {
            _httpClient = httpClientFactory.CreateClient();
            _payOS = payOS;
        }

        public async Task<string> Handle(CreatePayOSOrderCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return string.Empty;

            int orderCode = int.Parse(TimeHelper.GetTimeNow().ToString("ffffff"));
            ItemData item = new ItemData("Mì tôm hảo hảo ly", 1, 1000);
            List<ItemData> items = new List<ItemData> { item };

            // Get the current request's base URL
            var baseUrl = "";

            PaymentData paymentData = new PaymentData(
                orderCode,
                Convert.ToInt32(request.Price),
                request.Description,
                items,
                $"{baseUrl}/cancel",
                $"{baseUrl}/success"
            );

            CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

            return createPayment.checkoutUrl;
        }
    }
}
