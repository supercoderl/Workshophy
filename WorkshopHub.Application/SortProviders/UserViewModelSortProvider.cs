using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels.Users;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.SortProviders
{
    public sealed class UserViewModelSortProvider : ISortingExpressionProvider<UserViewModel, User>
    {
        private static readonly Dictionary<string, Expression<Func<User, object>>> s_expressions = new()
    {
        { "email", user => user.Email },
        { "firstName", user => user.FirstName },
        { "lastName", user => user.LastName },
        { "lastloggedindate", user => user.LastLoggedinDate ?? DateTimeOffset.MinValue },
        { "role", user => user.Role },
        { "status", user => user.Status },
        { "phoneNumber", user => user.PhoneNumber }
    };

        public Dictionary<string, Expression<Func<User, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
