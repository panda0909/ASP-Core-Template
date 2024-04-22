using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Core_Template.Services
{
    public interface ITemplateService
    {
        string GetMessage();
        string GetAppsetting(string key);
    }
    public class TemplateService : ITemplateService
    {
        private readonly IConfiguration _configuration; //配置文件

        public TemplateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetMessage()
        {
            return "Hello from the TemplateService!";
        }
        public string GetAppsetting(string key)
        {
            string value = _configuration[key];
            if (value == null)
            {
                return "";
            }
            return value;
        }
    }
}