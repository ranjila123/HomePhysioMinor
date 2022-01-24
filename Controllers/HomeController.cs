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

        public IActionResult Index()
        {
            //var test = new PatientViewModel();
            //test.Name1 = "Urja";
            //var test1= _mapper.Map<PatientModel>(test); // PatientModel is destination

            //var test = new PhysiotherapistViewModel(); // data input from form
            //test.Name1 = "Monika";
            //test.Address = "Lagan";
            //var test1 = _mapper.Map<PhysiotherapistModel>(test); // PatientModel is destination
            //var test1 = new PhysiotherapistModel { 
            // Name= test.Name1,
            // Address = test.Address
            //};
            //_applicationDbContext.PhysiotherapistModel.Add(test1);
            //ViewBag.Gender = new SelectList(_applicationDbContext.GenderModel.ToList(), nameof(GenderModel.GenderId),nameof(GenderModel.TypeName));
            //ViewBag.Urja = new {test = "a" };
            //ViewBag.Urja = "asd";
            //ViewBag.Urja = 0;

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
                return View(nameof(Physio_Profile_Page));
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


        public IActionResult Physio_Profile_Page()
        {
            return View();
        }
    }
}
