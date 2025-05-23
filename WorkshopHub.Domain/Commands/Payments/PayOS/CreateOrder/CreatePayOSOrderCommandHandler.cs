using MediatR;
using Microsoft.Extensions.Options;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Helpers;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Domain.Settings;

namespace WorkshopHub.Domain.Commands.Payments.PayOS.CreateOrder
{
    public sealed class CreatePayOSOrderCommandHandler : CommandHandlerBase, IRequestHandler<CreatePayOSOrderCommand, string>
    {
        private readonly HttpClient _httpClient;
        private readonly Net.payOS.PayOS _payOS;
        private readonly PayOsSettings _payOsSettings;

        public CreatePayOSOrderCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IHttpClientFactory httpClientFactory,
            Net.payOS.PayOS payOS,
            IOptions<PayOsSettings> payOsSettings
        ) : base(bus , unitOfWork, notifications ) 
        {
            _httpClient = httpClientFactory.CreateClient();
            _payOS = payOS;
            _payOsSettings = payOsSettings.Value;
        }

        public async Task<string> Handle(CreatePayOSOrderCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return string.Empty;

            int orderCode = int.Parse(TimeHelper.GetTimeNow().ToString("ffffff"));

            PaymentData paymentData = new PaymentData(
                orderCode,
                Convert.ToInt32(request.Price),
                request.Description,
                request.Items,
                $"{_payOsSettings.BaseUrl}/cancel",
                $"{_payOsSettings.BaseUrl}/success",
                CreateSignature(
                    request.Price.ToString(),
                    $"{_payOsSettings.BaseUrl}/cancel",
                    request.Description,
                    orderCode.ToString(),
                    $"{_payOsSettings.BaseUrl}/success",
                    _payOsSettings.ChecksumKey
                ),
                request.BuyerName,
                request.BuyerEmail,
                request.BuyerPhone,
                request.BuyerAddress,
                Convert.ToInt32(new DateTimeOffset(TimeHelper.GetTimeNow().AddMinutes(5).ToUniversalTime()).ToUnixTimeSeconds())
            );

            CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

            return createPayment.checkoutUrl;
        }

        private string CreateSignature(string amount, string cancelUrl, string description, string orderCode, string returnUrl, string checksumKey)
        {
            var data = $"amount={amount}&cancelUrl={cancelUrl}&description={description}&orderCode={orderCode}&returnUrl={returnUrl}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(checksumKey));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            var signature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return signature;
        }
    }
}
