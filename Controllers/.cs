using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Controllers
{
    public class _ : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
