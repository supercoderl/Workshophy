using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WorkshopHub.Domain.Models;

namespace WorkshopHub.Application.Interfaces
{
    public interface ITemplateService
    {
        Task<string> GetHtmlBodyFromTemplateAsync(string templateName);
    }
}
