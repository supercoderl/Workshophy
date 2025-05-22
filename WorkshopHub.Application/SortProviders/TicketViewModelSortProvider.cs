using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels.Tickets;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.SortProviders
{
    public sealed class TicketViewModelSortProvider : ISortingExpressionProvider<TicketViewModel, Ticket>
    {
        private static readonly Dictionary<string, Expression<Func<Ticket, object>>> s_expressions = new()
        {

        };

        public Dictionary<string, Expression<Func<Ticket, object>>> GetSortingExpressions()
        {
            return s_expressions;
        }
    }
}
