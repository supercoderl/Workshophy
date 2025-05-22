using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Constanst
{
    public static class MaxLengths
    {
        public static class User
        {
            public const int Email = 320;
            public const int FirstName = 100;
            public const int LastName = 100;
            public const int Password = 128;
        }
    }
}
