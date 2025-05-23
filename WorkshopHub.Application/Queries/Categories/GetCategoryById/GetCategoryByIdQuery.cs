using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Categories;

namespace WorkshopHub.Application.Queries.Categories.GetCategoryById
{
    public sealed record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryViewModel?>;
}
