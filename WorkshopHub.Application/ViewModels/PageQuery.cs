﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels
{
    public sealed class PageQuery
    {
        private int _page = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = Math.Max(0, value);
        }

        public int Page
        {
            get => _page;
            set => _page = Math.Max(1, value);
        }
    }
}
