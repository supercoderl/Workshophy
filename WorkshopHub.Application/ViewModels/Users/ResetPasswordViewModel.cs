using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels.Users
{
    public sealed record ResetPasswordViewModel(string OTP, string NewPassword);
}
