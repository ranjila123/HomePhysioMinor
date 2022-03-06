using AutoMapper;
using HomePhysio.Data;
using HomePhysio.Models;
using HomePhysio.Services.FileUpload;
using HomePhysio.Services.Payment;
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
        private readonly IPaymentService _paymentService;
        private readonly IFileUpload _fileUpload;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager; //usermanager depend on identity user


        public HomeController(IPaymentService paymentService, IFileUpload fileUpload, ILogger<HomeController> logger, IMapper mapper, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole>roleManager)
        {
            _paymentService = paymentService;
            _fileUpload = fileUpload;
            _applicationDbContext = applicationDbContext;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
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

                var imageProfile = _applicationDbContext.PhysioImage.Where(x => x.ImgId == 1 && x.PhysiotherapistId == item.PhysiotherapistId).FirstOrDefault();
                if (imageProfile != null)
                    item.PImg = imageProfile.Image;
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
                CategoryList = x.physioCategoryModels.ToList(),
                ConsultationCharge = x.ConsultationCharge,
                Email = x.UserData.Email,
                Age = x.age
            }).SingleOrDefault(o=>o.PhysiotherapistId== physiotherapistId);
            var physioImg = _applicationDbContext.PhysioImage.FirstOrDefault(x => x.ImgId == 1 && x.PhysiotherapistId == physio.PhysiotherapistId);
            if (physioImg != null)
            {
                physio.PImg = physioImg.Image;
            }
            return View(physio);
        }

        public async Task<IActionResult> PhysioAppointmentTable()
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

        public async Task<IActionResult> AdminAppointmentTable()
        {
           
            return View(await _applicationDbContext.AppointmentsModels.Include(x=> x.PatientData).Include(x=>x.StatusData).Include(x=>x.PhysioTimeSlotsData).ThenInclude(x=>x.PhysiotherapistData).ToListAsync());

        }

        public IActionResult Payment()
        {
            return View();
        }
        public async Task<IActionResult> Profile_Page()
        {
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var physio = await _applicationDbContext.PhysiotherapistModel.AnyAsync(o => o.UserId == user.Id);
            if (await _userManager.IsInRoleAsync(user,"Admin")){
                return RedirectToAction(nameof(CategoryModelsController.Index), "CategoryModels");
            }
            else if (physio == false)
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
                patient.PImg = patientImg.Image;


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
            var app = _applicationDbContext.AppointmentsModels.AsNoTracking().Where(x => x.PatientId == patientId).Include(x => x.PhysioTimeSlotsData).ThenInclude(x => x.PhysiotherapistData).Include(x => x.StatusData).Join(_applicationDbContext.PaymentsModel, x => x.AppointmentId, y => y.AppointmentId, (x, y) => new PatientProfileVM
            {
                DateAndTime = x.PhysioTimeSlotsData.DateTimeShift,
                Date = x.PhysioTimeSlotsData.DateTimeShift.Date.ToString("yyyy/MM/dd"),
                Time = x.PhysioTimeSlotsData.DateTimeShift.ToShortTimeString(),
                AppointmentId = x.AppointmentId,
                Physiotherapist = x.PhysioTimeSlotsData.PhysiotherapistData.Name,
                Status = x.StatusData.StatusType,
                Amount = y.Amount + y.DistanceAmount,
                DistanceAmount = y.DistanceAmount,
                ConsultationAmount = y.Amount,
                PContactNo=x.PhysioTimeSlotsData.PhysiotherapistData.ContactNo,
            }).ToList();

            return Json(new { ad = app });

        }

        [HttpGet]
        public async Task<IActionResult> Physio_Profile_Page()
        {
            //var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            //var physio = await _applicationDbContext.PhysiotherapistModel.Include(x => x.GenderData).Include(x => x.UserData).Include(x => x.physioCategoryModels).ThenInclude(x => x.Category).SingleOrDefaultAsync(o => o.UserId == user.Id);
            //return View(physio);

            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var physioOrg = await _applicationDbContext.PhysiotherapistModel.Where(x => x.UserId == user.Id).Include(x => x.GenderData).Include(x => x.physioCategoryModels).ThenInclude(x => x.Category).SingleOrDefaultAsync();
            if(physioOrg.CStatusModel=="0")
            {
                if(_applicationDbContext.CitizenshipTable.Where(x=>x.CitizenShipNumber==physioOrg.CitizenshipNumber).Count()>0)
                {
                    physioOrg.CStatusModel = "1";
                }
            }
            if (physioOrg.LStatusModel == "0")
            {
                if (_applicationDbContext.LicenseTable.Where(x => x.LicenseNumber == physioOrg.LicenseNo).Count() > 0)
                {
                    physioOrg.CStatusModel = "1";
                }
            }
            await _applicationDbContext.SaveChangesAsync();
            var physio =  new PhysiotherapistVM
            {
                UserId = user.Id,
                PhysiotherapistId = physioOrg.PhysiotherapistId,
                GenderTypeName = physioOrg.GenderData.TypeName,
                Name = physioOrg.Name,
                Age = physioOrg.age,
                ContactNo = physioOrg.ContactNo,
                Address = physioOrg.Address,
                Qualification = physioOrg.Qualification,
                CategoryList = physioOrg.physioCategoryModels.ToList(),
                CStatusModel = physioOrg.CStatusModel,
                LStatusModel = physioOrg.LStatusModel,
                CitizenshipNumber = physioOrg.CitizenshipNumber,
                LicenseNo = physioOrg.LicenseNo
            };
            var physioImg = _applicationDbContext.PhysioImage.FirstOrDefault(x => x.ImgId == 1 && x.PhysiotherapistId == physio.PhysiotherapistId);
            if (physioImg != null)
            {
                physio.PImg = physioImg.Image;
            } 
            var physioImg1 = _applicationDbContext.PhysioImage.FirstOrDefault(x => x.ImgId == 2 && x.PhysiotherapistId == physio.PhysiotherapistId);
            if (physioImg1 != null)
            {
                physio.PImg1 = physioImg1.Image;
            } 
            var physioImg2 = _applicationDbContext.PhysioImage.FirstOrDefault(x => x.ImgId == 3 && x.PhysiotherapistId == physio.PhysiotherapistId);
            if (physioImg2 != null)
            {
                physio.PImg2 = physioImg2.Image;
            } 
            var physioImg3 = _applicationDbContext.PhysioImage.FirstOrDefault(x => x.ImgId == 4 && x.PhysiotherapistId == physio.PhysiotherapistId);
            if (physioImg3 != null)
            {
                physio.PImg3 = physioImg3.Image;
            } 
            var physioImg4 = _applicationDbContext.PhysioImage.FirstOrDefault(x => x.ImgId == 5 && x.PhysiotherapistId == physio.PhysiotherapistId);
            if (physioImg4 != null)
            {
                physio.PImg4 = physioImg4.Image;
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

            var Physioapp = _applicationDbContext.AppointmentsModels.AsNoTracking().Where(x => x.PhysioTimeSlotsData.PhysiotherapistId == physiotherapistId).Include(x => x.StatusData).Include(x => x.PatientData).Include(x => x.PhysioTimeSlotsData).ThenInclude(x => x.PhysiotherapistData).ToList().Select(x => new PhysiotherapistProfileVM
            {
                AppointmentId = x.AppointmentId,
                DateAndTime = x.PhysioTimeSlotsData.DateTimeShift,
                Date = x.PhysioTimeSlotsData.DateTimeShift.Date.ToString("yyyy/MM/dd"),
                Time = x.PhysioTimeSlotsData.DateTimeShift.ToShortTimeString(),
                PAddress=x.PatientData.Address,
                PatientName = x.PatientData.Name,
                Status = x.StatusData.StatusType,
                PLongitude=x.PatientData.Longitude,
                PLatitude=x.PatientData.Latitude,
                PContactNo=x.PatientData.PhoneNo,
            });
            return Json(new { ap = Physioapp });
        }

        [HttpPost]
        public async Task<JsonResult> UploadPatientImage(IFormFile file, string imageType)
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
        public async Task<JsonResult> UploadPhysioImage(IFormFile file, string imageType)
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
        public async Task<IActionResult> KhaltiConfirm(PayLoadVM payload,int appointmentId)
        {
            var result = await _paymentService.KhaltiPay(new PayloadPostVM
            {
                amount = payload.amount,
                token = payload.token
            });
            if (result.state != null && result.state.name == "Completed")
            {
                var appointment =await _applicationDbContext.AppointmentsModels.SingleOrDefaultAsync(x => x.AppointmentId == appointmentId);
                appointment.PaidAmount = appointment.PaidAmount + payload.amount;
                appointment.StatusCode = "4";
                await _applicationDbContext.SaveChangesAsync();
                return Json(new { result = true, msg = "Payment Successful" });
            }
            else
                return Json(new { result = false, msg = "Payment Error" });

        }

        [HttpPost]
        public async Task<JsonResult> GetAppointmentData()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var customerData = _applicationDbContext.AppointmentsModels.Include(x => x.PhysioTimeSlotsData).ThenInclude(x => x.PhysiotherapistData).Include(x => x.PatientData).Include(x=>x.StatusData).Where(m => m.PhysioTimeSlotsData.DateTimeShift.ToString().Contains(searchValue)).ToList();
               
                recordsTotal = customerData.Count();
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new
                {
                    draw = draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal = recordsTotal,
                    data = data.Select(x => new AllAppointmentView
                    {
                        AppointmentId = x.AppointmentId,
                        PhysiotherapistName = x.PhysioTimeSlotsData.PhysiotherapistData.Name,
                        PatientName = x.PatientData.Name,
                        DateAndTime = x.PhysioTimeSlotsData.DateTimeShift,
                        StatusType = x.StatusData.StatusType
                    })
                };
                return Json(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
