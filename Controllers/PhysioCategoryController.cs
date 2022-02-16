using AutoMapper;
using HomePhysio.Data;
using HomePhysio.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class PhysioCategoryController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager; //usermanager depend on identity user
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _applicationDbContext;

        public PhysioCategoryController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, ApplicationDbContext applicationDbContext, IMapper mapper)
        {

            _userManager = userManager;
            _mapper = mapper;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            return View(await _applicationDbContext.PhysioCategoryModel.Where(x => x.Physiotherapist.UserId == user.Id).Include(x => x.Category).Include(x => x.Physiotherapist).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Category = new SelectList(_applicationDbContext.CategoryModel.ToList(), nameof(CategoryModel.CategoryId), nameof(CategoryModel.Name));

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PhysioCategoryModel physioCategoryModel)
        {
            if (ModelState.IsValid)
            {
                //this.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
                var physio = await _applicationDbContext.PhysiotherapistModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
                physioCategoryModel.PhysiotherapistId = physio.PhysiotherapistId;
                if (_applicationDbContext.PhysioCategoryModel.Where(x => x.PhysiotherapistId == physio.PhysiotherapistId && x.CategoryId == physioCategoryModel.CategoryId).Count() == 0)
                {
                    _applicationDbContext.Add(physioCategoryModel);
                    await _applicationDbContext.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(physioCategoryModel);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var physioCategoryModel = await _applicationDbContext.PhysioCategoryModel.FindAsync(id);
            if (physioCategoryModel == null)
            {
                return NotFound();
            }

            //ViewBag.Category = new SelectList(_applicationDbContext.CategoryModel.ToList(), nameof(CategoryModel.CategoryId), nameof(CategoryModel.Name));
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var physio = await _applicationDbContext.PhysiotherapistModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var physioCategory = await _applicationDbContext.PhysioCategoryModel.Include(x=>x.Category).SingleOrDefaultAsync(x => x.Id == id);

            if (physio.PhysiotherapistId == physioCategory.PhysiotherapistId)
            {
                return View(physioCategoryModel);
            }
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PhysioCategoryModel physioCategoryModel)
        {
            if (id != physioCategoryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
                var physio = await _applicationDbContext.PhysiotherapistModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
                var physioCategory = await _applicationDbContext.PhysioCategoryModel.SingleOrDefaultAsync(x => x.Id == id);

                if (physio.PhysiotherapistId == physioCategory.PhysiotherapistId)
                {
                    physioCategory.Experience = physioCategoryModel.Experience;

                    await _applicationDbContext.SaveChangesAsync();
                }

                
                return RedirectToAction(nameof(Index));
            }
            return View(physioCategoryModel);

        }
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Details(PhysioCategoryModel physioCategoryModel)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = _applicationDbContext.PhysioCategoryModel.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _applicationDbContext.PhysioCategoryModel.Remove(obj);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}
