using AutoMapper;
using HomePhysio.Data;
using HomePhysio.Models;
using HomePhysio.Services.FileUpload;
using HomePhysio.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IFileUpload _fileUpload;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<HomeController>_logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager; //usermanager depend on identity user


        public HomeController(IFileUpload fileUpload, ILogger<HomeController> logger,IMapper mapper, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _fileUpload = fileUpload;
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public IActionResult Dropdown1()
        {
           
            return View();
        }
        
        public IActionResult DropDown1Test()
        {
            var physio = _applicationDbContext.PhysiotherapistModel.Include(x => x.GenderData).ToList().Select(x => new PhysiotherapistVM
            {
                PhysiotherapistId = x.PhysiotherapistId,
                Name = x.Name,
                Address = x.Address,
                //GenderTypeName=new GenderModel { TypeName = x.Physiotherapist.GenderData.TypeName },
                GenderTypeName = x.GenderData.TypeName,
                ContactNo = x.ContactNo,
                Qualification = x.Qualification,
            }).ToList();
            foreach (var item in physio)
            {
                var catList = _applicationDbContext.PhysioCategoryModel.Where(x => x.PhysiotherapistId == item.PhysiotherapistId).Include(x => x.Category);
                foreach (var catItem in catList)
                {
                    item.Category = item.Category + $", {catItem.Category.Name} {catItem.Experience}";
                }
            }
            return Json(new { pList = physio });
        }

        [Authorize(Roles = "Patient")]
        public IActionResult Dropdown2()
        {
            ViewBag.categoryList = _applicationDbContext.CategoryModel.ToList();
            
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
       
        public IActionResult Physio_info(int physiotherapistId)
        {
            var physio = _applicationDbContext.PhysiotherapistModel.Include(x => x.GenderData).Include(x=>x.physioCategoryModels).ThenInclude(x=>x.Category).Select(x => new PhysiotherapistVM
            {
                PhysiotherapistId = x.PhysiotherapistId,
                Name = x.Name,
                Address = x.Address,
                //GenderTypeName=new GenderModel { TypeName = x.Physiotherapist.GenderData.TypeName },
                GenderTypeName = x.GenderData.TypeName,
                ContactNo = x.ContactNo,
                Qualification = x.Qualification,
                CategoryList = x.physioCategoryModels.ToList()
            }).SingleOrDefault(o=>o.PhysiotherapistId== physiotherapistId);
            var physioImg = _applicationDbContext.PhysioImage.FirstOrDefault(x => x.ImgId == 1 && x.PhysiotherapistId == physio.PhysiotherapistId);
            if (physioImg != null)
            {
                physio.PImg = physioImg.Image;
            }
            return View(physio);
        }

        public async Task <IActionResult> PhysioAppointmentTable()
        {
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var physio = await _applicationDbContext.PhysiotherapistModel.Include(x => x.GenderData).Include(x => x.UserData).Include(x => x.physioCategoryModels).ThenInclude(x => x.Category).SingleOrDefaultAsync(o => o.UserId == user.Id);
            return View(physio);
        }

        public async Task<IActionResult> PatientAppointmentTable()
        {
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var patient = await _applicationDbContext.PatientModel.Include(x => x.GenderData).Include(x => x.UserData).SingleOrDefaultAsync(o => o.UserId == user.Id);
            return View(patient);
           
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
            //var pa = await _applicationDbContext.PatientModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var patient = _applicationDbContext.PatientModel.Include(x => x.GenderData).Select(x => new PatientViewModel
            {
                UserId = x.UserId,
                PatientId = x.PatientId,
                Name1 = x.Name,
                Age = x.Age,
                GenderTypeName = x.GenderData.TypeName,
                Email = x.Email,
                PhoneNo = x.PhoneNo,
                Address = x.Address
            }).SingleOrDefault(x => x.UserId == user.Id);
            var patientImg = _applicationDbContext.PatientImage.FirstOrDefault(x => x.ImgId == 1 && x.PatientId == patient.PatientId);
            if (patientImg != null)
                patient.PImg= patientImg.Image;
                

            return View(patient);
        }

        [HttpPost]
        public IActionResult AppointmentList(int patientId)
        {
            //var app = await _applicationDbContext.AppointmentsModels.Where(x => x.PatientId == id).Include(x => x.PhysioTimeSlotsData).ThenInclude(x => x.PhysiotherapistData).ToList().Select(x => new PatientProfileVM
            //{
            //    DateAndTime=x.PhysioTimeSlotsData.DateTimeShift,


            //}).ToList();
            //var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
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
            //var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            //var physio = await _applicationDbContext.PhysiotherapistModel.Include(x => x.GenderData).Include(x => x.UserData).Include(x => x.physioCategoryModels).ThenInclude(x => x.Category).SingleOrDefaultAsync(o => o.UserId == user.Id);
            //return View(physio);

            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var physio = await _applicationDbContext.PhysiotherapistModel.Where(x => x.UserId == user.Id).Include(x => x.GenderData).Include(x => x.physioCategoryModels).ThenInclude(x => x.Category).Select(x => new PhysiotherapistVM
            {
                UserId = user.Id,
                PhysiotherapistId = x.PhysiotherapistId,
                GenderTypeName = x.GenderData.TypeName,
                Name = x.Name,
                Age = x.age,
                ContactNo = x.ContactNo,
                Address = x.Address,
                Qualification = x.Qualification,
                CategoryList=x.physioCategoryModels.ToList()

            }).SingleOrDefaultAsync();
            var physioImg = _applicationDbContext.PhysioImage.FirstOrDefault(x => x.ImgId == 1 && x.PhysiotherapistId == physio.PhysiotherapistId);
            if (physioImg != null)
            {
                physio.PImg = physioImg.Image;
            }
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

        [HttpPost]
        public async Task<JsonResult> UploadPatientImage(IFormFile file,string imageType)
        {
            var data = _fileUpload.ImageToByte(file);
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var patient = await _applicationDbContext.PatientModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var imageModelDb = _applicationDbContext.PatientImage.SingleOrDefault(x => x.PatientId == patient.PatientId && x.ImgId == int.Parse(imageType));
            if (imageModelDb != null)
            {
                imageModelDb.Image = data;
            }
            else
            {
                var imageModel = new PatientImage
                {
                    PatientId = patient.PatientId,
                    ImgId = int.Parse(imageType),
                    Image = data,
                };
                _applicationDbContext.PatientImage.Add(imageModel);
            }
            await _applicationDbContext.SaveChangesAsync();
            return Json(new { });
        }
        [HttpPost]
        public async Task<JsonResult> UploadPhysioImage(IFormFile file,string imageType)
        {
            var data = _fileUpload.ImageToByte(file);
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var physio = await _applicationDbContext.PhysiotherapistModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var imageModelDb = _applicationDbContext.PhysioImage.SingleOrDefault(x => x.PhysiotherapistId == physio.PhysiotherapistId && x.ImgId == int.Parse(imageType));
            if (imageModelDb != null)
            {
                imageModelDb.Image = data;
            }
            else
            {
                var imageModel = new PhysioImage
                {
                    PhysiotherapistId = physio.PhysiotherapistId,
                    ImgId = int.Parse(imageType),
                    Image = data,
                };
                _applicationDbContext.PhysioImage.Add(imageModel);
            }
            await _applicationDbContext.SaveChangesAsync();
            return Json(new { });
        }

        public IActionResult About()
        {
            return View("~/Views/Public/About.cshtml");
        }
        
        public IActionResult FAQ()
        {
            return View("~/Views/Public/FAQ.cshtml");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
