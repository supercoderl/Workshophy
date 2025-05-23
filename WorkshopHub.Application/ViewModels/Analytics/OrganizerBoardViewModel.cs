using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Reviews;
using WorkshopHub.Application.ViewModels.Workshops;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.ViewModels.Analytics
{
    public sealed class OrganizerBoardViewModel
    {
        public int TotalTicketBoughtForOwnWorkshop { get; set; }
        public ReviewViewModel? CurrentReview { get; set; } = new ReviewViewModel();
        public WorkshopViewModel? BestRatingWorkshop { get; set; } = new WorkshopViewModel();
        public decimal Revenue {  get; set; }

        public static OrganizerBoardViewModel FromOrganizerBoard(Review? review, Workshop? workshop)
        {
            return new OrganizerBoardViewModel
            {
                TotalTicketBoughtForOwnWorkshop = 0,
                CurrentReview = review != null ? ReviewViewModel.FromReview(review) : null,
                BestRatingWorkshop = workshop != null ? WorkshopViewModel.FromWorkshop(workshop) : null,
                Revenue = 0
            };
        }
    }
}
