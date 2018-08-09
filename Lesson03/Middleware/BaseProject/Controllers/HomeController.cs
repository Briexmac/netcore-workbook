using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BaseProject.Models;
using System.Diagnostics.Contracts;
using BaseProject.Intrastructure;

namespace BaseProject.Controllers
{
    
    [Route("Home")]
    public class HomeController : Controller
    {
        private static string[] allowedUser = new[]
        {
            "Bryanna"
        };
        [Route("")]      // Combines to define the route template "Home"
        [Route("Index")] // Combines to define the route template "Home/Index"
        [Route("/")]     // Doesn't combine, defines the route template ""
        public IActionResult Index([FromQuery] string username)
        {
            Contract.Ensures(Contract.Result<IActionResult>() != null);
            if (allowedUser.Contains(username))
                return View();
            else
                throw new NotFoundException(username);
        }

        [Route("About")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
