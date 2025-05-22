using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels.Reviews
{
    public class ReviewFilter
    {
        public int? Star { get; set; }
        public int? HelpfulCount { get; set; }
        public DateTime? Date { get; set; }
    }
}
