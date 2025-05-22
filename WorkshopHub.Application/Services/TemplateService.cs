using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Domain.Models;

namespace WorkshopHub.Application.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly string _templateDirectory;

        public TemplateService(
            string templateDirectory
        )
        {
            _templateDirectory = templateDirectory;
        }

        public async Task<string> GetHtmlBodyFromTemplateAsync(string templateName)
        {
            var htmlPath = Path.Combine(_templateDirectory, $"{templateName}.cshtml");
            var textPath = Path.Combine(_templateDirectory, $"{templateName}.txt");

            if (!File.Exists(htmlPath))
            {
                throw new FileNotFoundException($"Email template not found: {templateName}");
            }

            return await File.ReadAllTextAsync(htmlPath);
        }
    }
}
