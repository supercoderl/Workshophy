﻿using MediatR;
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
            string htmlBody = """
                <!DOCTYPE html>
            <html lang="en">
            <head>
                <meta charset="UTF-8">
                <meta name="viewport" content="width=device-width, initial-scale=1.0">
                <title>Ticket Confirmation - {{EventName}}</title>
            </head>
            <body style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; line-height: 1.6; color: #333; background-color: #f4f4f4; margin: 0; padding: 0;">
                <div style="max-width: 600px; margin: 0 auto; background-color: #ffffff; box-shadow: 0 0 10px rgba(0,0,0,0.1);">
                    <!-- Header -->
                    <div style="background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 30px; text-align: center;">
                        <h1 style="margin: 0; font-size: 28px; font-weight: 300;">Ticket Confirmation</h1>
                        <div style="background-color: #28a745; color: white; padding: 8px 16px; border-radius: 20px; display: inline-block; margin-top: 10px; font-size: 14px; font-weight: bold;">✓ CONFIRMED</div>
                    </div>

                    <!-- Main Content -->
                    <div style="padding: 40px 30px;">
                        <div style="font-size: 18px; margin-bottom: 20px; color: #2c3e50;">
                            Hello {{UserName}},
                        </div>

                        <p>Great news! Your ticket purchase has been confirmed. We're excited to see you at the event!</p>

                        <!-- Ticket Details -->
                        <table width="100%" style="margin-bottom: 12px; border-bottom: 1px solid #e9ecef;">
                            <tr>
                                <td style="font-weight: 600; color: #495057; width: 30%;">Event:</td>
                                <td style="text-align: right; color: #2c3e50;">{{EventName}}</td>
                            </tr>

                            <tr>
                                <td style="font-weight: 600; color: #495057; width: 30%;">Date & Time:</td>
                                <td style="text-align: right; color: #2c3e50;">{{EventDateTime}}</td>
                            </tr>

                            <tr>
                                <td style="font-weight: 600; color: #495057; width: 30%;">Venue:</td>
                                <td style="text-align: right; color: #2c3e50;">{{VenueName}}</td>
                            </tr>

                            <tr>
                                <td style="font-weight: 600; color: #495057; width: 30%;">Address:</td>
                                <td style="text-align: right; color: #2c3e50;">{{VenueAddress}}</td>
                            </tr>

                            <tr>
                                <td style="font-weight: 600; color: #495057; width: 30%;">Ticket Type:</td>
                                <td style="text-align: right; color: #2c3e50;">{{TicketType}}</td>
                            </tr>

                            <tr>
                                <td style="font-weight: 600; color: #495057; width: 30%;">Quantity:</td>
                                <td style="text-align: right; color: #2c3e50;">{{TicketQuantity}}</td>
                            </tr>

                            <tr>
                                <td style="font-weight: 600; color: #495057; width: 30%;">Total Amount:</td>
                                <td style="text-align: right; color: #2c3e50;">{{TotalAmount}}</td>
                            </tr>

                            <tr>
                                <td style="font-weight: 600; color: #495057; width: 30%;">Confirmation #:</td>
                                <td style="text-align: right; color: #2c3e50;">{{ConfirmationNumber}}</td>
                            </tr>
                        </table>

                        <!-- QR Code Section -->
                        <div style="text-align: center; margin: 30px 0; padding: 20px; background-color: #fff; border: 2px dashed #dee2e6; border-radius: 8px;">
                            <h3>Your Digital Ticket</h3>
                            <p>Show this QR code at the entrance:</p>
                            <img src="{{QRCodeImage}}" alt="QR Code" style="max-width: 150px; height: auto; margin: 10px 0;" />
                            <p><small>Ticket ID: {{TicketId}}</small></p>
                        </div>

                        <!-- Instructions -->
                        <div style="background-color: #e3f2fd; border-left: 4px solid #2196f3; padding: 15px; margin: 20px 0; border-radius: 4px;">
                            <h3 style="margin-top: 0; color: #1976d2;">Important Information</h3>
                            <ul>
                                <li><strong>Arrive Early:</strong> Please arrive at least 30 minutes before the event starts.</li>
                                <li><strong>Bring ID:</strong> A valid photo ID may be required for entry.</li>
                                <li><strong>Digital Ticket:</strong> Save this email or screenshot the QR code for easy access.</li>
                                <li><strong>No Refunds:</strong> Please review our refund policy on our website.</li>
                            </ul>
                        </div>

                        <p>If you have any questions or need to make changes to your booking, please contact our support team at <a href="mailto:{{SupportEmail}}" style="color: #3498db; text-decoration: none;">{{SupportEmail}}</a> or call {{SupportPhone}}.</p>

                        <p>Thank you for your purchase, and we look forward to seeing you at {{EventName}}!</p>

                        <p>
                            Best regards,<br>
                            <strong>The {{OrganizationName}} Team</strong>
                        </p>
                    </div>

                    <!-- Footer -->
                    <div style="background-color: #2c3e50; color: #ecf0f1; padding: 30px; text-align: center;">
                        <p>&copy; {{CurrentYear}} {{OrganizationName}}. All rights reserved.</p>

                        <div style="margin: 15px 0;">
                            <a href="{{FacebookUrl}}" style="display: inline-block; margin: 0 10px; color: #bdc3c7; font-size: 14px; text-decoration: none;">Facebook</a>
                            <a href="{{TwitterUrl}}" style="display: inline-block; margin: 0 10px; color: #bdc3c7; font-size: 14px; text-decoration: none;">Twitter</a>
                            <a href="{{InstagramUrl}}" style="display: inline-block; margin: 0 10px; color: #bdc3c7; font-size: 14px; text-decoration: none;">Instagram</a>
                        </div>

                        <p>
                            <a href="{{WebsiteUrl}}" style="color: #3498db; text-decoration: none;">Visit our website</a> |
                            <a href="mailto:{{SupportEmail}}" style="color: #3498db; text-decoration: none;">Contact Support</a> |
                            <a href="{{UnsubscribeUrl}}" style="color: #3498db; text-decoration: none;">Unsubscribe</a>
                        </p>
                    </div>
                </div>
            </body>
            </html>
            """;
            await _paymentService.HandleResponseAsync(body, htmlBody);
            return Response(new
            {
                IsSuccess = true
            });
        }
    }
}

                