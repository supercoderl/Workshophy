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
            var htmlBody = await _templateService.GetHtmlBodyFromTemplateAsync("Otp");

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
