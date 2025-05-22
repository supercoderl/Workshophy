using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Models;

namespace WorkshopHub.Application.ViewModels.Users
{
    public sealed record ForgotPasswordViewModel(string Email);
}
