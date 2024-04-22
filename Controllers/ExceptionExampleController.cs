using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASP_Core_Template.ExceptionFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASP_Core_Template.Controllers
{
    [Route("[controller]")]
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public class ExceptionExampleController : Controller
    {
        private readonly ILogger<ExceptionExampleController> _logger;

        public ExceptionExampleController(ILogger<ExceptionExampleController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}