using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Commands.Users.CreateUser;
using WorkshopHub.Domain.EventHandler.Fanout;
using WorkshopHub.Domain.EventHandler;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Shared.Events.User;
using WorkshopHub.Domain.Commands.Users.UpdateUser;
using WorkshopHub.Domain.Commands.Users.DeleteUser;
using WorkshopHub.Domain.Commands.Users.Login;
using WorkshopHub.Domain.Commands.Users.ForgotPassword;
using WorkshopHub.Domain.Commands.Users.Logout;
using WorkshopHub.Domain.Commands.Users.RefreshToken;
using WorkshopHub.Domain.Commands.Users.ResetPassword;
using WorkshopHub.Domain.Commands.RefreshTokens;
using WorkshopHub.Shared.Events.RefreshToken;
using WorkshopHub.Shared.Events.PasswordResetToken;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using WorkshopHub.Domain.Commands.Mail.SendMail;
using WorkshopHub.Domain.Settings;
using Microsoft.Extensions.Configuration;
using WorkshopHub.Domain.Commands.Workshops.CreateWorkshop;
using WorkshopHub.Domain.Commands.Workshops.UpdateWorkshop;
using WorkshopHub.Domain.Commands.Workshops.DeleteWorkshop;
using WorkshopHub.Domain.Commands.Tickets.CreateTicket;
using WorkshopHub.Domain.Commands.Tickets.UpdateTicket;
using WorkshopHub.Domain.Commands.Tickets.DeleteTicket;
using WorkshopHub.Domain.Commands.Bookings.CreateBooking;
using WorkshopHub.Domain.Commands.Payments.PayOS.CreateOrder;
using WorkshopHub.Domain.Commands.Reviews.CreateReview;
using WorkshopHub.Domain.Commands.Reviews.UpdateReview;
using WorkshopHub.Domain.Commands.Reviews.DeleteReview;
using WorkshopHub.Domain.Commands.Badges.CreateBadge;
using WorkshopHub.Domain.Commands.Badges.UpdateBadge;
using WorkshopHub.Domain.Commands.Badges.DeleteBadge;
using WorkshopHub.Domain.Commands.BlogPosts.CreateBlogPost;
using WorkshopHub.Domain.Commands.BlogPosts.UpdateBlogPost;
using WorkshopHub.Domain.Commands.BlogPosts.DeleteBlogPost;
using WorkshopHub.Domain.Commands.Categories.CreateCategory;
using WorkshopHub.Domain.Commands.Categories.UpdateCategory;
using WorkshopHub.Domain.Commands.Categories.DeleteCategory;
using Net.payOS;
using Microsoft.Extensions.Options;
using WorkshopHub.Domain.Commands.Workshops.ApproveWorkshop;

namespace WorkshopHub.Domain.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services)
        {
            // User
            services.AddScoped<IRequestHandler<CreateUserCommand>, CreateUserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand>, UpdateUserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand>, DeleteUserCommandHandler>();
            services.AddScoped<IRequestHandler<LoginCommand, object>, LoginCommandHandler>();
            services.AddScoped<IRequestHandler<ForgotPasswordCommand>, ForgotPasswordCommandHandler>();
            services.AddScoped<IRequestHandler<LogoutCommand>, LogoutCommandHandler>();
            services.AddScoped<IRequestHandler<RefreshTokenCommand, object>, RefreshTokenCommandHandler>();
            services.AddScoped<IRequestHandler<ResetPasswordCommand>, ResetPasswordCommandHandler>();

            // Refresh Token
            services.AddScoped<IRequestHandler<CreateRefreshTokenCommand>, CreateRefreshTokenCommandHandler>();

            // Email
            services.AddScoped<IRequestHandler<SendMailCommand>, SendMailCommandHandler>();

            // Workshop
            services.AddScoped<IRequestHandler<CreateWorkshopCommand>, CreateWorkshopCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateWorkshopCommand>, UpdateWorkshopCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteWorkshopCommand>, DeleteWorkshopCommandHandler>();
            services.AddScoped<IRequestHandler<ApproveWorkshopCommand>, ApproveWorkshopCommandHandler>();

            // Ticket
            services.AddScoped<IRequestHandler<CreateTicketCommand>, CreateTicketCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTicketCommand>, UpdateTicketCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteTicketCommand>, DeleteTicketCommandHandler>();

            // Booking
            services.AddScoped<IRequestHandler<CreateBookingCommand, string>, CreateBookingCommandHandler>();

            // Payment
            services.AddScoped<IRequestHandler<CreatePayOSOrderCommand, string>, CreatePayOSOrderCommandHandler>();

            // Review
            services.AddScoped<IRequestHandler<CreateReviewCommand>,  CreateReviewCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateReviewCommand>, UpdateReviewCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteReviewCommand>, DeleteReviewCommandHandler>();

            // Badge
            services.AddScoped<IRequestHandler<CreateBadgeCommand>, CreateBadgeCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateBadgeCommand>, UpdateBadgeCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteBadgeCommand>, DeleteBadgeCommandHandler>();

            // Blog Post
            services.AddScoped<IRequestHandler<CreateBlogPostCommand>, CreateBlogPostCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateBlogPostCommand>, UpdateBlogPostCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteBlogPostCommand>, DeleteBlogPostCommandHandler>();

            // Category
            services.AddScoped<IRequestHandler<CreateCategoryCommand>, CreateCategoryCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCategoryCommand>, UpdateCategoryCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteCategoryCommand>, DeleteCategoryCommandHandler>();

            return services;
        }

        public static IServiceCollection AddNotificationHandlers(this IServiceCollection services)
        {
            // Fanout
            services.AddScoped<IFanoutEventHandler, FanoutEventHandler>();

            // User
            services.AddScoped<INotificationHandler<UserCreatedEvent>, UserEventHandler>();
            services.AddScoped<INotificationHandler<UserUpdatedEvent>, UserEventHandler>();
            services.AddScoped<INotificationHandler<UserDeletedEvent>, UserEventHandler>();
            services.AddScoped<INotificationHandler<PasswordResetTokenCreatedEvent>, UserEventHandler>();

            // Refresh Token
            services.AddScoped<INotificationHandler<RefreshTokenCreatedEvent>, RefreshTokenEventHandler>();

            return services;
        }

        public static IServiceCollection AddApiUser(this IServiceCollection services)
        {
            // User
            services.AddScoped<IUser, ApiUser>();

            return services;
        }

        public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
        {
            // Register settings
            services
               .AddOptions<SmtpSettings>()
               .Bind(configuration.GetSection("Smtp"))
               .ValidateOnStart();

            return services;
        }

        public static IServiceCollection AddPayOs(this IServiceCollection services, IConfiguration configuration)
        {
            // Register settings
            services
               .AddOptions<PayOsSettings>()
               .Bind(configuration.GetSection("PayOS"))
               .ValidateOnStart();

            services.AddSingleton(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<PayOsSettings>>().Value;
                return new PayOS(
                    settings.ClientID,
                    settings.ApiKey,
                    settings.ChecksumKey
                );
            });

            return services;
        }
    }
}
