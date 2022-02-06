using AutoMapper;
using HomePhysio.Data;
using HomePhysio.Models;
using HomePhysio.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<HomeController>_logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager; //usermanager depend on identity user


        public HomeController(ILogger<HomeController> logger,IMapper mapper, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }
       
        public IActionResult Dropdown1()
        {
            return View();
        }
        public IActionResult Dropdown2()
        {
            ViewBag.categoryList = _applicationDbContext.CategoryModel.ToList();
            
            return View();
        }

        public IActionResult Login()
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
        public IActionResult Payment()
        {
            return View();
        }
        public async Task<IActionResult> Profile_Page()
        {
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var physio = await  _applicationDbContext.PhysiotherapistModel.AnyAsync(o=> o.UserId==user.Id );
            if (physio == false)
            {
                return RedirectToAction("Patient_Profile_Page");
            }
            else
            {
                return RedirectToAction("Physio_Profile_Page");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Patient_Profile_Page()
        {
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var patient = await _applicationDbContext.PatientModel.SingleOrDefaultAsync(o => o.UserId == user.Id);
            return View(patient);
        }

        [HttpPost]
        public IActionResult AppointmentList(int patientId)
        {
            //var app = await _applicationDbContext.AppointmentsModels.Where(x => x.PatientId == id).Include(x => x.PhysioTimeSlotsData).ThenInclude(x => x.PhysiotherapistData).ToList().Select(x => new PatientProfileVM
            //{
            //    DateAndTime=x.PhysioTimeSlotsData.DateTimeShift,


            //}).ToList();
            //var user =await _userManager.FindByNameAsync(this.User.Identity.Name);
            //var patient = await _applicationDbContext.PatientModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var app = _applicationDbContext.AppointmentsModels.AsNoTracking().Where(x => x.PatientId == patientId).Include(x => x.PhysioTimeSlotsData).ThenInclude(x => x.PhysiotherapistData).Include(x=>x.StatusData).ToList().Select(x => new PatientProfileVM
            {
                DateAndTime = x.PhysioTimeSlotsData.DateTimeShift,
                Date = x.PhysioTimeSlotsData.DateTimeShift.Date.ToString("yyyy/MM/dd"),
                Time = x.PhysioTimeSlotsData.DateTimeShift.TimeOfDay.ToString(),
                AppointmentId=x.AppointmentId,
                Physiotherapist=x.PhysioTimeSlotsData.PhysiotherapistData.Name,
                Status=x.StatusData.StatusType
                
            });
            return Json(new { ad =app});

        }

        [HttpGet]
        public async Task<IActionResult> Physio_Profile_Page()
        {
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var physio = await _applicationDbContext.PhysiotherapistModel.Include(x=>x.GenderData).Include(x=>x.UserData).Include(x=>x.physioCategoryModels).ThenInclude(x=>x.Category).SingleOrDefaultAsync(o => o.UserId == user.Id);
            return View(physio);
        }

        [HttpPost]
        public IActionResult PhysioAppointmentList(int physiotherapistId)
        {
            //var app = await _applicationDbContext.AppointmentsModels.Where(x => x.PatientId == id).Include(x => x.PhysioTimeSlotsData).ThenInclude(x => x.PhysiotherapistData).ToList().Select(x => new PatientProfileVM
            //{
            //    DateAndTime=x.PhysioTimeSlotsData.DateTimeShift,


            //}).ToList();
            //var user =await _userManager.FindByNameAsync(this.User.Identity.Name);
            //var patient = await _applicationDbContext.PatientModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
           
            var Physioapp = _applicationDbContext.AppointmentsModels.AsNoTracking().Where(x => x.PhysioTimeSlotsData.PhysiotherapistId== physiotherapistId).Include(x => x.StatusData).Include(x=>x.PatientData).Include(x => x.PhysioTimeSlotsData).ThenInclude(x => x.PhysiotherapistData).ToList().Select(x => new PhysiotherapistProfileVM
            {
                AppointmentId = x.AppointmentId,
                DateAndTime = x.PhysioTimeSlotsData.DateTimeShift,
                Date = x.PhysioTimeSlotsData.DateTimeShift.Date.ToString("yyyy/MM/dd"),
                Time = x.PhysioTimeSlotsData.DateTimeShift.TimeOfDay.ToString(),
                
                PatientName = x.PatientData.Name,
                Status = x.StatusData.StatusType

            });
            return Json(new { ap = Physioapp });

        }


        //public IActionResult Physio_Profile_Page()
        //{
        //    return View();
        //}
    }
}
