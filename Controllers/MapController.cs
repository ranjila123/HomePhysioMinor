using HomePhysio.Data;
using HomePhysio.Models;
using HomePhysio.ViewModel;
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
           // var physio = await _applicationDbContext.PhysiotherapistModel.ToListAsync();
            List<PhysiotherapistVM> physioList = new List<PhysiotherapistVM>();
            var physio = _applicationDbContext.PhysiotherapistModel.Include(x => x.GenderData).ToList().Select(x => new PhysiotherapistVM
            {
                PhysiotherapistId = x.PhysiotherapistId,
                Name = x.Name,
                Address = x.Address,
                //GenderTypeName=new GenderModel { TypeName = x.Physiotherapist.GenderData.TypeName },
                GenderTypeName = x.GenderData.TypeName,
                ContactNo = x.ContactNo,
                Qualification = x.Qualification,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
            }).ToList();
            foreach (var item1 in physio)
            {
                var distance = calcCrow(double.Parse(patient.Latitude), double.Parse(patient.Longitude), double.Parse(item1.Latitude), double.Parse(item1.Longitude));
                if (distance < 5)
                {
                    physioList.Add(item1);
                }
            }

            foreach (var item in physioList)
            {
                var catList = _applicationDbContext.PhysioCategoryModel.Where(x => x.PhysiotherapistId == item.PhysiotherapistId).Include(x => x.Category);
                foreach (var catItem in catList)
                {
                    item.Category = item.Category + $", {catItem.Category.Name} {catItem.Experience}";
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
