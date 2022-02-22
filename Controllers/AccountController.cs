using AutoMapper;
using HomePhysio.Data;
using HomePhysio.Models;
using HomePhysio.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager; //usermanager depend on identity user
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _applicationDbContext;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, ApplicationDbContext applicationDbContext,IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AdminRegister(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            RegisterAdminViewModel registerAdminViewModel = new RegisterAdminViewModel();//datatype variable =new object

            return View(registerAdminViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminRegister(RegisterAdminViewModel model, string returnurl = null)
        {
           // ViewData["ReturnUrl"] = returnurl;
            //returnurl = returnurl ?? Url.Content("~/");
            //server side validation
            if (ModelState.IsValid)
            {
                

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email};
                //will assign the incoming data from form into the user object data
                //creating a user
                var result = await _userManager.CreateAsync(user, model.Password);//directly hash the password
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    //for confirming user email
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackurl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                  

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction(nameof(CategoryModelsController.Index), "CategoryModels");

                    //return LocalRedirect(returnurl);
                   
                }
                AddErrors(result);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AdminLogin(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }

        //to login the user if already an account exists
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(LoginAdminViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {

                    if (returnurl == null)
                    {
                        return RedirectToAction("Index", "Public");
                    }
                    else
                    {
                        return LocalRedirect(returnurl);
                    }

                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attepmt.");
                    return View(model);
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register(string returnurl=null)
        {
            ViewData["ReturnUrl"] = returnurl;
            RegisterPatientViewModel registerViewModel = new RegisterPatientViewModel();//datatype variable =new object
            ViewBag.Gender = new SelectList(_applicationDbContext.GenderModel.ToList(), nameof(GenderModel.GenderId), nameof(GenderModel.TypeName));

            return View(registerViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterPatientViewModel model,string returnurl=null)
        {
            //ViewData["ReturnUrl"] = returnurl;
            //returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                //server side validation
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Patient");

                    var patient = _mapper.Map<PatientModel>(model);

                    patient.UserId = user.Id;

                    //add to patient table
                    _applicationDbContext.PatientModel.Add(patient);
                    await _applicationDbContext.SaveChangesAsync();

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackurl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                    await _emailSender.SendEmailAsync(model.Email, "Confirm your account - HomePhysio",
                        "Hey new User!.<br> Please confirm your account by clicking here: <a class=\"btn btn-primary\" href=\"" + callbackurl + "\">Back to HomePhysio</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnurl);
                    return RedirectToAction(nameof(HomeController.Profile_Page), "Home");


                    //return View("ConfirmEmailConfirmation");
                }
                AddErrors(result);
            }
            ViewBag.Gender = new SelectList(_applicationDbContext.GenderModel.ToList(), nameof(GenderModel.GenderId), nameof(GenderModel.TypeName));
            // RegisterViewModel registerViewModel = new RegisterViewModel();//datatype variable =new object
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RegisterPhysio(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            RegisterPhysioViewModel registerViewModel = new RegisterPhysioViewModel();//datatype variable =new object
            ViewBag.Gender = new SelectList(_applicationDbContext.GenderModel.ToList(), nameof(GenderModel.GenderId),nameof(GenderModel.TypeName));

            return View(registerViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterPhysio(RegisterPhysioViewModel model, string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                //server side validation
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Physiotherapist");

                    var physio = _mapper.Map<PhysiotherapistModel>(model);
                    physio.UserId = user.Id;

                    //add to patient table
                    _applicationDbContext.PhysiotherapistModel.Add(physio);
                    await _applicationDbContext.SaveChangesAsync();

                    //add to physiotherapist table
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackurl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                    // await _emailSender.SendEmailAsync(model.Email, "Confirm your account - HomePhysio",
                    //    "Hey new User!.<br> Please confirm your account by clicking here: <a class=\"btn btn-primary\" href=\"" + callbackurl + "\">Back to HomePhysio</a>");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnurl);
                    return RedirectToAction(nameof(HomeController.Profile_Page), "Home");

                    //return View("ConfirmEmailConfirmation");
                }
                AddErrors(result);
            }
            // RegisterViewModel registerViewModel = new RegisterViewModel();//datatype variable =new object
            ViewBag.Gender = new SelectList(_applicationDbContext.GenderModel.ToList(), nameof(GenderModel.GenderId), nameof(GenderModel.TypeName));


            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> EditPhysio(int? id)
        {   
            if(id ==null)
            {
                return View("Error");
            }
            var physio = await _applicationDbContext.PhysiotherapistModel.FindAsync(id);
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            ViewBag.Gender = new SelectList(_applicationDbContext.GenderModel.ToList(), nameof(GenderModel.GenderId), nameof(GenderModel.TypeName));
            var physioviewmodel = new EditPhysioViewModel
            {
                Email = user.Email,
                Name = physio.Name,
                Address = physio.Address,
                age = physio.age,
                GenderId = physio.GenderId,
                ContactNo = physio.ContactNo,
                //CitizenshipNumber = physio.CitizenshipNumber,
                //LicenseNo = physio.LicenseNo,
                Qualification = physio.Qualification,
                Experience = physio.Experience,
                ConsultationCharge = physio.ConsultationCharge,
                Longitude =physio.Longitude,
                Latitude = physio.Latitude,
                

            };
            if(physio != null)
            {
                return View(physioviewmodel);
            }
            return RedirectToAction(nameof(HomeController.Physio_Profile_Page), "Home");
        }

        [HttpPost]
        public async Task<IActionResult> EditPhysio(EditPhysioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
                var physioexiting = await _applicationDbContext.PhysiotherapistModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
                var physioin = _mapper.Map<PhysiotherapistModel>(model);
                //if(physioin.PhysiotherapistId == physioexiting.PhysiotherapistId)
                //{
                physioin.PhysiotherapistId = physioexiting.PhysiotherapistId;
                    _applicationDbContext.Update(physioin);
                    await _applicationDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(HomeController.Physio_Profile_Page), "Home");

                 //}
                return View(model);
            }
            ViewBag.Gender = new SelectList(_applicationDbContext.GenderModel.ToList(), nameof(GenderModel.GenderId), nameof(GenderModel.TypeName));
            return View(model);
        }

        public IActionResult RegisterOption()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult>ConfirmEmail(string userId,string code)
        {
            if(userId==null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        public IActionResult Login(string returnurl=null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnurl=null)
        {
           // ViewData["ReturnUrl"] = returnurl;
           // returnurl = returnurl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    //return LocalRedirect(returnurl);
                    return RedirectToAction(nameof(HomeController.Profile_Page), "Home");

                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return View(model);
                }
                
            }
            
            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackurl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);

                await _emailSender.SendEmailAsync(model.Email,"Reset Password - Identity Manager",
                    "Please reset your password by clicking here: <a href=\"" + callbackurl + "\">Back to the Website.</a>");
                //"Please reset your password by clicking here: <a href=\"" + callbackurl+ "\">link</a>");

                return RedirectToAction("ForgotPasswordConfirmation");
            } 

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string code=null)
        {
            return code==null? View("Error"): View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }

                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
                AddErrors(result);
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(PublicController.Index), "Public");
        }

        [HttpGet]
        public async Task<IActionResult> EnableAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            var token = await _userManager.GetAuthenticatorKeyAsync(user);
            var model = new TwoFactorAuthenticationViewModel() { Token = token };
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EnableAuthenticator(TwoFactorAuthenticationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var succeeded = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, model.Code);
                if (succeeded)
                {
                    await _userManager.SetTwoFactorEnabledAsync(user, true);
                }
                else
                {
                    ModelState.AddModelError("Verify", "Your two factor auth code can not be validated.");
                    return View(model);
                }
            }
            return RedirectToAction("AuthenticatorConfirmation");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
