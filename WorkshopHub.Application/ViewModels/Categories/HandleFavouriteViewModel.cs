﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels.Categories
{
    public sealed record HandleFavouriteViewModel
    (
        ICollection<Guid> CategoryIds
    );
}
