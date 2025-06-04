using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Categories.HandleFavourite
{
    public sealed class HandleFavouriteCommandValidation : AbstractValidator<HandleFavouriteCommand>
    {
        public HandleFavouriteCommandValidation()
        {
            
        }
    }
}
