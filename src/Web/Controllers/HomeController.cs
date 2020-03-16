using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainingTask.Web.Models;

namespace TrainingTask.Controllers
{
    public class HomeController : Controller
    {
        private ILogger Logger;
        public HomeController(ILogger fileLogger)
        {
            Logger = fileLogger;
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
        }

        public IActionResult Index()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            Logger.LogDebug($"{this.GetType().ToString()}.{new StackTrace(false).GetFrame(0).GetMethod().Name} is called");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
