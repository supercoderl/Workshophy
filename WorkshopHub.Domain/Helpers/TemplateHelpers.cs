using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Helpers
{
    public static class TemplateHelpers
    {
        public static string ProcessTemplate(string templateContent, Dictionary<string, string> templateData)
        {
            if (templateData == null || !templateData.Any())
                return templateContent;

            var result = templateContent;

            foreach (var item in templateData)
            {
                result = result.Replace($"{{{{{item.Key}}}}}", item.Value);
            }

            return result;
        }
    }
}
