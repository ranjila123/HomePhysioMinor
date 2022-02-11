using HomePhysio.Data;
using HomePhysio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Controllers
{
    public class MapController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public MapController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        public static double toRad(double Value)
        {
            return (Value * Math.PI / 180);
        }

        public double calcCrow(double latt1, double lon1,double latt2,double lon2)
        {
            var R = 6371; // km
            var dLat = toRad(latt2 - latt1);
            var dLon = toRad(lon2 - lon1);
            var lat1 = toRad(latt1);
            var lat2 = toRad(latt2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;
            return d;
        }

        [HttpPost]
        public async Task<IActionResult> Calculate()
        {
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var patient = await _applicationDbContext.PatientModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var physio = await _applicationDbContext.PhysiotherapistModel.ToListAsync();
            List<PhysiotherapistModel> physioList = new List<PhysiotherapistModel>();
            foreach(var item in physio)
            {
               var distance= calcCrow(double.Parse(patient.Latitude), double.Parse(patient.Longitude), double.Parse(item.Latitude), double.Parse(item.Longitude));
                if (distance < 5)
                {
                    physioList.Add(item);
                }
            }
            return Json(new { p = physioList });

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
