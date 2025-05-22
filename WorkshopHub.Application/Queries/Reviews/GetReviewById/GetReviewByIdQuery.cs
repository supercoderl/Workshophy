using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Reviews;

namespace WorkshopHub.Application.Queries.Reviews.GetReviewById
{
    public sealed record GetReviewByIdQuery(Guid Id) : IRequest<ReviewViewModel?>;
}
