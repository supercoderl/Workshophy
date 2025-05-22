using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels.Workshops
{
    public sealed class WorkshopFilter
    {
        public Guid? CategoryId { get; set; }
        public string? Location { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool IsOnSale { get; set; } = false;
    }
}
