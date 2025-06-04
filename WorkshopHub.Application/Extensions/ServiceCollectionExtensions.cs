using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.Queries.Users.GetAll;
using WorkshopHub.Application.Queries.Users.GetUserById;
using WorkshopHub.Application.Services;
using WorkshopHub.Application.SortProviders;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Users;
using WorkshopHub.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using WorkshopHub.Application.Queries.Workshops.GetWorkshopById;
using WorkshopHub.Application.ViewModels.Workshops;
using WorkshopHub.Application.Queries.Workshops.GetAll;
using WorkshopHub.Application.Queries.Tickets.GetTicketById;
using WorkshopHub.Application.ViewModels.Tickets;
using WorkshopHub.Application.Queries.Tickets.GetAll;
using WorkshopHub.Application.Queries.Reviews.GetReviewById;
using WorkshopHub.Application.ViewModels.Reviews;
using WorkshopHub.Application.Queries.Reviews.GetAll;
using WorkshopHub.Application.Queries.Badges.GetBadgeById;
using WorkshopHub.Application.ViewModels.Badges;
using WorkshopHub.Application.Queries.Badges.GetAll;
using WorkshopHub.Application.Queries.BlogPosts.GetBlogPostById;
using WorkshopHub.Application.Queries.BlogPosts.GetAll;
using WorkshopHub.Application.ViewModels.BlogPosts;
using WorkshopHub.Application.Queries.Categories.GetCategoryById;
using WorkshopHub.Application.ViewModels.Categories;
using WorkshopHub.Application.Queries.Categories.GetAll;
using WorkshopHub.Application.Queries.Analytics.GetAdminBoard;
using WorkshopHub.Application.ViewModels.Analytics;
using WorkshopHub.Application.Queries.Analytics.GetOrganizerBoard;
using WorkshopHub.Application.Queries.Workshops.GetWorkshopsByCategories;

namespace WorkshopHub.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWorkshopService, WorkshopService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IBadgeService, BadgeService>();
            services.AddScoped<IBlogPostService, BlogPostService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAnalysService, AnalysService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IMailService, MailService>();

            services.AddSingleton<ITemplateService>(provider => {
                var env = provider.GetService<IWebHostEnvironment>();
                var templateDirectory = Path.Combine(env?.ContentRootPath ?? string.Empty, "Templates/Mails");
                return new TemplateService(templateDirectory);
            });

            return services;
        }

        public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
        {
            // User
            services.AddScoped<IRequestHandler<GetUserByIdQuery, UserViewModel?>, GetUserByIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllUsersQuery, PagedResult<UserViewModel>>, GetAllUsersQueryHandler>();

            // Workshop
            services.AddScoped<IRequestHandler<GetWorkshopByIdQuery, WorkshopViewModel?>, GetWorkshopByIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllWorkshopsQuery, PagedResult<WorkshopViewModel>>, GetAllWorkshopsQueryHandler>();
            services.AddScoped<IRequestHandler<GetWorkshopsByCategoriesQuery, PagedResult<WorkshopViewModel>>, GetWorkshopsByCategoriesQueryHandler>();

            // Ticket
            services.AddScoped<IRequestHandler<GetTicketByIdQuery, TicketViewModel?>, GetTicketByIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllTicketsQuery, PagedResult<TicketViewModel>>, GetAllTicketsQueryHandler>();

            // Review
            services.AddScoped<IRequestHandler<GetReviewByIdQuery, ReviewViewModel?>, GetReviewByIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllReviewsQuery, PagedResult<ReviewViewModel>>, GetAllReviewsQueryHandler>();

            // Badge
            services.AddScoped<IRequestHandler<GetBadgeByIdQuery, BadgeViewModel?>, GetBadgeByIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllBadgesQuery, PagedResult<BadgeViewModel>>, GetAllBadgesQueryHandler>();

            // BlogPost
            services.AddScoped<IRequestHandler<GetBlogPostByIdQuery, BlogPostViewModel?>, GetBlogPostByIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllBlogPostsQuery, PagedResult<BlogPostViewModel>>, GetAllBlogPostsQueryHandler>();

            // Category
            services.AddScoped<IRequestHandler<GetCategoryByIdQuery, CategoryViewModel?>, GetCategoryByIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllCategoriesQuery, PagedResult<CategoryViewModel>>, GetAllCategoriesQueryHandler>();

            // Analys
            services.AddScoped<IRequestHandler<GetAdminBoardQuery, AdminBoardViewModel>, GetAdminBoardQueryHandler>();
            services.AddScoped<IRequestHandler<GetOrganizerBoardQuery, OrganizerBoardViewModel>, GetOrganizerBoardQueryHandler>();

            return services;
        }

        public static IServiceCollection AddSortProviders(this IServiceCollection services)
        {
            services.AddScoped<ISortingExpressionProvider<UserViewModel, User>, UserViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<WorkshopViewModel, Workshop>, WorkshopViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<TicketViewModel, Ticket>, TicketViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<ReviewViewModel, Review>, ReviewViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<BadgeViewModel, Badge>, BadgeViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<BlogPostViewModel, BlogPost>, BlogPostViewModelSortProvider>();
            services.AddScoped<ISortingExpressionProvider<CategoryViewModel, Category>, CategoryViewModelSortProvider>();

            return services;
        }
    }
}
