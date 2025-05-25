using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;

namespace WorkshopHub.Domain.Commands.Payments.PayOS.HandleRespose
{
    public sealed class HandleResponseCommandValidation : AbstractValidator<HandleResponseCommand>
    {
        public HandleResponseCommandValidation()
        {
            
        }
    }
}
