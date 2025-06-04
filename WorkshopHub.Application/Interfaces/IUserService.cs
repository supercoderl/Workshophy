using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels.Users;
using WorkshopHub.Domain.Models;

namespace WorkshopHub.Application.Interfaces
{
    public interface IUserService
    {
        public Task<UserViewModel?> GetUserByUserIdAsync(Guid userId);
        public Task<UserViewModel?> GetCurrentUserAsync();

        public Task<PagedResult<UserViewModel>> GetAllUsersAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null);

        public Task<Guid> CreateUserAsync(CreateUserViewModel user);
        public Task UpdateUserAsync(UpdateUserViewModel user);
        public Task DeleteUserAsync(Guid userId);
        public Task<object> LoginUserAsync(LoginUserViewModel viewModel);
        public Task ForgotPasswordAsync(ForgotPasswordViewModel viewModel, string htmlBody);
        public Task LogoutAsync(LogoutViewModel logout);
        public Task ResetPasswordAsync(ResetPasswordViewModel resetPassword);
        public Task<object> RefreshTokenAsync(RefreshTokenViewModel refreshToken);
        public Task<object> LoginGoogleAsync(LoginGoogleViewModel loginGoogleViewModel);
    }
}
