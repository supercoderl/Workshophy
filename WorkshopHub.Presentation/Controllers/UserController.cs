﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.RegularExpressions;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.SortProviders;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels.Users;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Models;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Presentation.Models;
using WorkshopHub.Presentation.Swagger;

namespace WorkshopHub.Presentation.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public sealed class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly ITemplateService _templateService;

        public UserController(
            INotificationHandler<DomainNotification> notifications,
            IUserService userService,
            ITemplateService templateService
        ) : base(notifications)
        {
            _userService = userService;
            _templateService = templateService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [SwaggerOperation("Get a list of all users")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<UserViewModel>>))]
        public async Task<IActionResult> GetAllUsersAsync(
            [FromQuery] PageQuery query,
            [FromQuery] string searchTerm = "",
            [FromQuery] bool includeDeleted = false,
            [FromQuery] [SortableFieldsAttribute<UserViewModelSortProvider, UserViewModel, User>]
            SortQuery? sortQuery = null
        )
        {
            var users = await _userService.GetAllUsersAsync(
                query,
                includeDeleted,
                searchTerm,
                sortQuery);
            return Response(users);
        }

        [Authorize("Admin")]
        [HttpGet("{id}")]
        [SwaggerOperation("Get a user by id")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UserViewModel>))]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid id)
        {
            var user = await _userService.GetUserByUserIdAsync(id);
            return Response(user);
        }

        [Authorize]
        [HttpGet("me")]
        [SwaggerOperation("Get the current active user")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UserViewModel>))]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var user = await _userService.GetCurrentUserAsync();
            return Response(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [SwaggerOperation("Create a new user")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserViewModel viewModel)
        {
            var userId = await _userService.CreateUserAsync(viewModel);
            return Response(userId);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Delete a user")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return Response(id);
        }

        [HttpPut]
        [SwaggerOperation("Update a user")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateUserViewModel>))]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserViewModel viewModel)
        {
            await _userService.UpdateUserAsync(viewModel);
            return Response(viewModel);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [SwaggerOperation("Get a signed token for a user")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<object>))]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserViewModel viewModel)
        {
            var token = await _userService.LoginUserAsync(viewModel);
            return Response(token);
        }

        [AllowAnonymous]
        [HttpPost("login/google")]
        [SwaggerOperation("Get a signed token for a user")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<object>))]
        public async Task<IActionResult> LoginGoogleAsync([FromBody] LoginGoogleViewModel viewModel)
        {
            var token = await _userService.LoginGoogleAsync(viewModel);
            return Response(token);
        }

        [Authorize]
        [HttpPost("logout")]
        [SwaggerOperation("Logout a user")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<string>))]
        public async Task<IActionResult> LogoutUserAsync([FromBody] LogoutViewModel viewModel)
        {
            await _userService.LogoutAsync(viewModel);
            return Response();
        }

        [AllowAnonymous]
        [HttpPost("forgot")]
        [SwaggerOperation("Send otp to forgot password user")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<string>))]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordViewModel viewModel)
        {
            string htmlBody = """
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
            await _userService.ForgotPasswordAsync(new ForgotPasswordViewModel(viewModel.Email), htmlBody);
            return Response();
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        [SwaggerOperation("Get a new signed token for a user")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<object>))]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenViewModel viewModel)
        {
            var token = await _userService.RefreshTokenAsync(viewModel);
            return Response(token);
        }

        [AllowAnonymous]
        [HttpPost("reset")]
        [SwaggerOperation("Reset password")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<string>))]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordViewModel viewModel)
        {
            await _userService.ResetPasswordAsync(viewModel);
            return Response();
        }
    }
}
