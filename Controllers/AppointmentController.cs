using HomePhysio.Data;
using HomePhysio.Models;
using HomePhysio.ViewModel;
using HomePhysio.ViewModel.DataTablesVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public AppointmentController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
      
        public IActionResult Index()
        {
            ViewBag.Categories =new SelectList( _applicationDbContext.CategoryModel.ToList(),nameof(CategoryModel.CategoryId),nameof(CategoryModel.Name));
            
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetPhysiotherapistList(int categoryId)
        {
            var physio = _applicationDbContext.PhysioCategoryModel.Where(x => x.CategoryId == categoryId).Include(x => x.Physiotherapist).ThenInclude(x => x.GenderData).ToList().Select(x => new PhysiotherapistVM
            {
                PhysiotherapistId = x.PhysiotherapistId,
                Name = x.Physiotherapist.Name,
                Address = x.Physiotherapist.Address,
                //GenderTypeName=new GenderModel { TypeName = x.Physiotherapist.GenderData.TypeName },
                GenderTypeName = x.Physiotherapist.GenderData.TypeName,
                ContactNo = x.Physiotherapist.ContactNo,
                Qualification=x.Physiotherapist.Qualification,

            }).ToList();
            foreach (var item in physio)
            {
                var catList = _applicationDbContext.PhysioCategoryModel.Where(x => x.PhysiotherapistId == item.PhysiotherapistId).Include(x => x.Category);
                foreach (var catItem in catList)
                {
                    item.Category = item.Category +$", {catItem.Category.Name} {catItem.Experience}";
                }
            }
            return Json(new { a = physio });

        }
        [HttpPost]
        public async Task<IActionResult> GetPhysiotherapistTimeSlot(int physiotherapistId)
        {
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var patient = await _applicationDbContext.PatientModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var physioTimeSlot = _applicationDbContext.PhysioTimeSlotsModel.Include(x => x.appointmentsModels).Where(x => x.PhysiotherapistId == physiotherapistId && x.appointmentsModels.Count(y => y.StatusCode == "1") == 0).Select(x => new PhysioTimeSlotForAppointmentVM
            {
                PhysioTimeSlotsId = x.PhysioTimeSlotsId,
                DateAndTime = x.DateTimeShift,
                Date = x.DateTimeShift.Date.ToString("yyyy/MM/dd"),
                Time = x.DateTimeShift.TimeOfDay.ToString(),
                StatusCode = x.appointmentsModels.Count(y=>y.PatientId== patient.PatientId && y.PhysioTimeSlotsId==x.PhysioTimeSlotsId)> 0 ? "2" : "0"
            }).ToList();

            //var appointment = _applicationDbContext.AppointmentsModels.ToList();
            //foreach (var item in appointment) {
               

            //    //Where(x => x.PhysioTimeSlotsId == item.PhysioTimeSlotsId).
            //}
            //var a = _applicationDbContext.PhysioTimeSlotsModel.SingleOrDefault();
            return Json(new { b = physioTimeSlot });
        }
        [HttpPost]
        public async Task<IActionResult> SaveAppointmentInfo(int physioTimeSlotsId)
        {
            try
            {
                if (_applicationDbContext.AppointmentsModels.Count(x => x.PhysioTimeSlotsId == physioTimeSlotsId && x.StatusCode == "1") == 0)
                {
                    var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
                    var patient = await _applicationDbContext.PatientModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
                    var app = new AppointmentsModel
                    {
                        PhysioTimeSlotsId = physioTimeSlotsId,
                        PatientId = patient.PatientId,
                        StatusCode = "2"

                    };
                    _applicationDbContext.AppointmentsModels.Add(app);
                    await _applicationDbContext.SaveChangesAsync();
                    return Json(new { result = true, msg = $"AppointentId:{app.AppointmentId}" });
                }
                else
                {
                    return Json(new { result = false, msg = $"Appoinment alread booked." });

                }
            }
            catch
            {
                return Json(new { result = false, msg = "Response False" });

            }
        }
    }
}
