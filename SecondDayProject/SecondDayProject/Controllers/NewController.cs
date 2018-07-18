using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecondDayProject.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecondDayProject.Controllers
{
    public class NewController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = new Greeting();
            model.CurrentTime = DateTime.Today.AddHours(5);
            model.Firstname = "Bryanna";
            return View(model);
        }
    }
}
