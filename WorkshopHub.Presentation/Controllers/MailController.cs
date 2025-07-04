using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.ViewModels.Mails;
using WorkshopHub.Domain.Models;
using WorkshopHub.Domain.Notifications;

namespace WorkshopHub.Presentation.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class MailController : ApiController
    {
        private readonly IMailService _mailService;
        private readonly ITemplateService _templateService;

        public MailController(
            INotificationHandler<DomainNotification> notifications,
            IMailService mailService,
            ITemplateService templateService
        ) : base(notifications)
        {
            _mailService = mailService;
            _templateService = templateService;
        }

        [HttpPost("TicketConfirmation")]
        public async Task<IActionResult> SendTicketConfirmationMailAsync()
        {
            var htmlBody = await _templateService.GetHtmlBodyFromTemplateAsync("TicketConfirmation");

            await _mailService.SendMailAsync(new SendMailViewModel(
                    "minh.quang1720@gmail.com",
                    "Test mail",
                    htmlBody,
                    true,
                    new Dictionary<string, string> {
                        { "UserName", "John Bob" },
                        { "EventName", "Tech & R" },
                        { "EventDateTime", "20/09/2025" },
                        { "VenueName", "H&B Coporation" },
                        { "VenueAddress", "12A, Jimston, New York" },
                        { "TicketType", "Workshop Ticket" },
                        { "TicketQuantity", "1" },
                        { "TotalAmount", "490000" },
                        { "ConfirmationNumber", "TKT-2024-001234" },
                        { "TicketId", "64ec02f8-603f-43ec-9f95-618fdf440562" },
                        { "OrganizationName", "Supercoderle" },
                        { "SupportEmail", "workshophy@tech.com" },
                        { "SupportPhone", "+1 289 740 729" },
                        { "CurrentYear", "2025" },
                        { "WebsiteUrl", "https://workshophy.com" },
                        { "FacebookUrl", "https://facebook.com/workshophy" },
                        { "TwitterUrl", "https://x.com/workshophy" },
                        { "InstagramUrl", "https://www.instagram.com/workshophy" },
                        { "UnsubscribeUrl", "https://buymeacoffee.com/workshophy" },
                        { "QRCodeImage", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/41/QR_Code_Example.svg/800px-QR_Code_Example.svg.png" }
                    },
                    new List<EmailAttachment>(),
                    new Dictionary<string, string> { }
                )
            );
            return Response(new
            {
                IsSuccess = true
            });
        }

        [HttpPost("Subcription")]
        public async Task<IActionResult> SendSubcriptionMailAsync()
        {
            var htmlBody = await _templateService.GetHtmlBodyFromTemplateAsync("Subcription");

            await _mailService.SendMailAsync(new SendMailViewModel(
                    "minh.quang1720@gmail.com",
                    "Test mail",
                    htmlBody,
                    true,
                    new Dictionary<string, string> {
                        { "UserName", "John Bob" },
                        { "PlanName", "Tech & R" },
                        { "StartDate", "20/09/2025" },
                        { "BillingCycle", "Online Bank" },
                        { "NextBillingDate", "31/09/2025" },
                        { "Amount", "490000" },
                        { "Feature1", "TKT-2024-001234" },
                        { "Feature2", "64ec02f8-603f-43ec-9f95-618fdf440562" },
                        { "Feature3", "Supercoderle" },
                        { "Feature4", "workshophy@tech.com" },
                        { "PlanBenefit", "Delivery" },
                        { "NextDeliveryDate", "01/10/2025" },
                        { "ManageSubscriptionLink", "https://workshophy.com" },
                        { "FacebookUrl", "https://facebook.com/workshophy" },
                        { "TwitterUrl", "https://x.com/workshophy" },
                        { "InstagramUrl", "https://www.instagram.com/workshophy" },
                        { "LinkedInUrl", "https://linkedin.com/workshophy" }
                    },
                    new List<EmailAttachment>(),
                    new Dictionary<string, string> { }
                )
            );
            return Response(new
            {
                IsSuccess = true
            });
        }

        [HttpPost("Otp")]
        public async Task<IActionResult> SendOtpMailAsync()
        {
            var htmlBody = """
                        <!DOCTYPE html>
            <html lang="en">
            <head>
                <meta charset="UTF-8">
                <meta name="viewport" content="width=device-width, initial-scale=1.0">
                <title>Your Verification Code</title>
            </head>
            <body style="font-family: Arial, sans-serif; background-color: #f5f5f5; margin: 0; padding: 20px; line-height: 1.6;">
                <div style="max-width: 600px; margin: 0 auto; background-color: white; border-radius: 8px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1); overflow: hidden;">

                    <!-- Header -->
                    <div style="background-color: #4285f4; padding: 30px; text-align: center;">
                        <img src="https://res.cloudinary.com/dcystvroz/image/upload/v1748060235/betdywvq4scxsm3lhhdu.png" alt="Company Logo" style="height: 80px; margin-bottom: 10px;">
                        <h1 style="color: white; margin: 0; font-size: 24px; font-weight: bold;">Verification Code</h1>
                    </div>

                    <!-- Content -->
                    <div style="padding: 40px;">
                        <h2 style="color: #333; text-align: center; font-size: 20px; margin-bottom: 20px;">Hello {{UserName}},</h2>

                        <p style="color: #666; text-align: center; margin-bottom: 30px; font-size: 16px;">
                            We received a request to verify your account. Use the verification code below to complete your verification:
                        </p>

                        <!-- OTP Display -->
                        <div style="background-color: #f8f9fa; border: 2px dashed #4285f4; border-radius: 8px; padding: 30px; text-align: center; margin: 30px 0;">
                            <p style="color: #666; margin-bottom: 10px; font-size: 14px; text-transform: uppercase; letter-spacing: 1px;">Your Verification Code</p>
                            <div style="font-size: 36px; font-weight: bold; color: #4285f4; letter-spacing: 8px; margin: 20px 0;">{{OTPCode}}</div>
                            <p style="color: #999; margin-top: 10px; font-size: 12px;">This code expires in {{ExpiryMinutes}} minutes</p>
                        </div>

                        <!-- Instructions -->
                        <div style="background-color: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 25px 0;">
                            <h3 style="color: #856404; margin-top: 0; font-size: 16px;">Important:</h3>
                            <ul style="color: #856404; margin-bottom: 0; padding-left: 20px;">
                                <li>Enter this code exactly as shown</li>
                                <li>Do not share this code with anyone</li>
                                <li>This code will expire in {{ExpiryMinutes}} minutes</li>
                                <li>If you didn't request this code, please ignore this email</li>
                            </ul>
                        </div>

                        <!-- Alternative Text Code -->
                        <div style="text-align: center; margin: 30px 0;">
                            <p style="color: #666; font-size: 14px; margin-bottom: 10px;">Having trouble? Copy and paste this code:</p>
                            <div style="background-color: #f1f3f4; border-radius: 4px; padding: 10px; font-family: monospace; font-size: 18px; font-weight: bold; color: #333; letter-spacing: 2px; word-break: break-all;">{{OTPCode}}</div>
                        </div>

                        <!-- Support Information -->
                        <div style="border-top: 1px solid #e0e0e0; padding-top: 20px; margin-top: 30px;">
                            <p style="color: #666; font-size: 14px; text-align: center; margin-bottom: 10px;">
                                Need help? Contact our support team at
                                <a href="mailto:{{SupportEmail}}" style="color: #4285f4; text-decoration: none;">{{SupportEmail}}</a>
                            </p>
                        </div>
                    </div>

                    <!-- Footer -->
                    <div style="background-color: #f8f9fa; padding: 20px; text-align: center; border-top: 1px solid #e0e0e0;">
                        <p style="color: #666; font-size: 12px; margin: 0;">
                            This email was sent to <strong>{{ReceiverEmail}}</strong>
                        </p>
                        <p style="color: #999; font-size: 12px; margin: 10px 0 0 0;">
                            © {{CurrentYear}} {{CompanyName}}. All rights reserved.<br>
                            {{CompanyAddress}}
                        </p>
                    </div>
                </div>
            </body>
            </html>
            """;

            await _mailService.SendMailAsync(new SendMailViewModel(
                    "minh.quang1720@gmail.com",
                    "Test mail",
                    htmlBody,
                    true,
                    new Dictionary<string, string> {
                        { "UserName", "John Bob" },
                        { "ReceiverEmail", "minh.quang1720@gmail.com" },
                        { "OTPCode", "098375" },
                        { "ExpiryMinutes", "6" },
                        { "CompanyName", "Workshophy" },
                        { "CompanyAddress", "123 Tran Phu Street" },
                        { "SupportEmail", "workshophy@tech.com" },
                        { "CurrentYear", "2025" }
                    },
                    new List<EmailAttachment>(),
                    new Dictionary<string, string> { }
                )
            );
            return Response(new
            {
                IsSuccess = true
            });
        }

        [HttpPost("Newsletter")]
        public async Task<IActionResult> SendNewsletterMailAsync()
        {
            var htmlBody = await _templateService.GetHtmlBodyFromTemplateAsync("Newsletter");

            await _mailService.SendMailAsync(new SendMailViewModel(
                    "minh.quang1720@gmail.com",
                    "Test mail",
                    htmlBody,
                    true,
                    new Dictionary<string, string> {
                        { "NewsletterTitle", "A new day again" },
                        { "IssueDate", "Issue #15 - March 2025" },
                        { "UserName", "John Bob" },
                        { "ArticleTitle1", "Online Bank" },
                        { "ArticleSummary1", "Trump will visit to Viet Nam. Let's check with him." },
                        { "ArticleLink1", "https://abc.com" },
                        { "ArticleTitle2", "Steam Collection" },
                        { "ArticleSummary2", "The new game is in order! Do you want to get it?" },
                        { "ArticleLink2", "https://steam.com" },
                        { "ArticleTitle3", "People on vacation day" },
                        { "ArticleSummary3", "Many people in the coresponse eat more food" },
                        { "ArticleLink3", "https://food.com" },
                        { "QuoteText", "Who is the best student today?" },
                        { "QuoteAuthor", "Phinial" },
                        { "CTATitle", "ABC" },
                        { "CTADescription", "ABC is a program about the zoo in Canaday, many food at there." },
                        { "CTALink", "https://linkedin.com/workshophy" },
                        { "CTAButtonText", "Get Started" },
                        { "UnsubscribeLink", "https://abc.com" }
                    },
                    new List<EmailAttachment>(),
                    new Dictionary<string, string> { }
                )
            );
            return Response(new
            {
                IsSuccess = true
            });
        }
    }
}
