using AutoMapper;
using HomePhysio.Data;
using HomePhysio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Controllers
{
    public class TimeSlotController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager; //usermanager depend on identity user
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _applicationDbContext;

        public TimeSlotController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, ApplicationDbContext applicationDbContext, IMapper mapper)
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
            var physio = await _applicationDbContext.PhysiotherapistModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
            return View(await _applicationDbContext.PhysioTimeSlotsModel.Where(x => x.PhysiotherapistId == physio.PhysiotherapistId).Include(x => x.PhysiotherapistData).ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PhysioTimeSlotsModel physioTimeSlotModel)
        {
            if (ModelState.IsValid)
            {
                //this.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
                var physio = await _applicationDbContext.PhysiotherapistModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
                physioTimeSlotModel.PhysiotherapistId = physio.PhysiotherapistId;
                if (_applicationDbContext.PhysioTimeSlotsModel.Where(x => x.PhysiotherapistId == physio.PhysiotherapistId && x.DateTimeShift == physioTimeSlotModel.DateTimeShift).Count() == 0)
                {
                    _applicationDbContext.Add(physioTimeSlotModel);
                    await _applicationDbContext.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(physioTimeSlotModel);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var physioTimeSlotsModel = await _applicationDbContext.PhysioTimeSlotsModel.FindAsync(id);
            if (physioTimeSlotsModel == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
            var physio = await _applicationDbContext.PhysiotherapistModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
            var physioTimeSlot = await _applicationDbContext.PhysioTimeSlotsModel.SingleOrDefaultAsync(x => x.PhysioTimeSlotsId == id);

            if (physio.PhysiotherapistId == physioTimeSlot.PhysiotherapistId)
            {
                return View(physioTimeSlotsModel);
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PhysioTimeSlotsModel physioTimeSlotsModel)
        {
            if (id != physioTimeSlotsModel.PhysioTimeSlotsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
                var physio = await _applicationDbContext.PhysiotherapistModel.SingleOrDefaultAsync(x => x.UserId == user.Id);
                var physioTimeSlot = await _applicationDbContext.PhysioTimeSlotsModel.SingleOrDefaultAsync(x => x.PhysioTimeSlotsId == id);

                if (physio.PhysiotherapistId == physioTimeSlot.PhysiotherapistId)
                {
                    physioTimeSlot.DateTimeShift = physioTimeSlotsModel.DateTimeShift;
                    await _applicationDbContext.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(physioTimeSlotsModel);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = _applicationDbContext.PhysioTimeSlotsModel.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _applicationDbContext.PhysioTimeSlotsModel.Remove(obj);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
