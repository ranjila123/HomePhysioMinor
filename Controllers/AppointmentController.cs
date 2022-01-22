using HomePhysio.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AppointmentController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
      
        public IActionResult Index()
        {
            var physio = _applicationDbContext.PhysiotherapistModel.ToList();
            
            return View(physio);
        }
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
