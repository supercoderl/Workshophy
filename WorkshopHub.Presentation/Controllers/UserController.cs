using MediatR;
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
            await _userService.ForgotPasswordAsync(new ForgotPasswordViewModel(viewModel.Email), await _templateService.GetHtmlBodyFromTemplateAsync("Otp"));
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
