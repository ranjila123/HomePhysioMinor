using HomePhysio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController>_logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
       
      
        public IActionResult Services()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult LearnMore()
        {
            return View();
        }

        public IActionResult Dropdown1()
        {
            return View();
        }
        public IActionResult Dropdown2()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult Terms()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Physio_info()
        {
            return View();
        }

        public IActionResult Book_appointment()
        {
            return View();
        }
        public IActionResult Profile_Page()
        {
            return View();
        }

    }
}
