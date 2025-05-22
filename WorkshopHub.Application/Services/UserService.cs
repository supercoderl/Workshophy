using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.Queries.Users.GetAll;
using WorkshopHub.Application.Queries.Users.GetUserById;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels.Users;
using WorkshopHub.Domain.Commands.Users.CreateUser;
using WorkshopHub.Domain.Commands.Users.DeleteUser;
using WorkshopHub.Domain.Commands.Users.ForgotPassword;
using WorkshopHub.Domain.Commands.Users.Login;
using WorkshopHub.Domain.Commands.Users.Logout;
using WorkshopHub.Domain.Commands.Users.RefreshToken;
using WorkshopHub.Domain.Commands.Users.ResetPassword;
using WorkshopHub.Domain.Commands.Users.UpdateUser;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Models;

namespace WorkshopHub.Application.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IMediatorHandler _bus;
        private readonly IUser _user;

        public UserService(
            IMediatorHandler bus, 
            IUser user
        )
        {
            _bus = bus;
            _user = user;
        }

        public async Task<UserViewModel?> GetUserByUserIdAsync(Guid userId)
        {
            return await _bus.QueryAsync(new GetUserByIdQuery(userId));
        }

        public async Task<UserViewModel?> GetCurrentUserAsync()
        {
            return await _bus.QueryAsync(new GetUserByIdQuery(_user.GetUserId()));
        }

        public async Task<PagedResult<UserViewModel>> GetAllUsersAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllUsersQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<Guid> CreateUserAsync(CreateUserViewModel user)
        {
            var userId = Guid.NewGuid();

            await _bus.SendCommandAsync(new CreateUserCommand(
                userId,
                user.Email,
                user.FirstName,
                user.LastName,
                user.Password,
                user.UserRole,
                user.Status
            ));

            return userId;
        }

        public async Task UpdateUserAsync(UpdateUserViewModel user)
        {
            await _bus.SendCommandAsync(new UpdateUserCommand(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                user.Role,
                user.Status
             ));
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            await _bus.SendCommandAsync(new DeleteUserCommand(userId));
        }

        public async Task<object> LoginUserAsync(LoginUserViewModel viewModel)
        {
            return await _bus.QueryAsync(new LoginCommand(viewModel.Email, viewModel.Password));
        }

        public async Task ForgotPasswordAsync(ForgotPasswordViewModel viewModel, string htmlBody)
        {
            await _bus.SendCommandAsync(new ForgotPasswordCommand(viewModel.Email, htmlBody));
        }

        public async Task LogoutAsync(LogoutViewModel logout)
        {
            await _bus.SendCommandAsync(new LogoutCommand(logout.Token));
        }

        public async Task ResetPasswordAsync(ResetPasswordViewModel resetPassword)
        {
            await _bus.SendCommandAsync(new ResetPasswordCommand(resetPassword.OTP, resetPassword.NewPassword));
        }

        public async Task<object> RefreshTokenAsync(RefreshTokenViewModel refreshToken)
        {
            return await _bus.QueryAsync(new RefreshTokenCommand(refreshToken.Token));
        }
    }
}
