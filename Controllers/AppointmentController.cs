using HomePhysio.Data;
using HomePhysio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.Categories =new SelectList( _applicationDbContext.CategoryModel.ToList(),nameof(CategoryModel.CategoryId),nameof(CategoryModel.Name));
            
            return View(physio);
        }
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetPhysiotherapistList(int categoryId)
        {
            var physio = _applicationDbContext.PhysioCategoryModel.Where(x=>x.CategoryId==categoryId).Include(x => x.Physiotherapist).ToList().Select(x=>new PhysiotherapistModel { 
                PhysiotherapistId = x.PhysiotherapistId,
                Name = x.Physiotherapist.Name
            });
            return Json(new { a = physio });

        }
    }
}
