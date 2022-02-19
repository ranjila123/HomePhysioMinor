using HomePhysio.Data;
using HomePhysio.Models;
using HomePhysio.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
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
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(int appointmentId)
        {
            var appointment = await _applicationDbContext.AppointmentsModels.Include(x=>x.PhysioTimeSlotsData).SingleOrDefaultAsync(x => x.AppointmentId == appointmentId);
            var patient = await _applicationDbContext.PatientModel.SingleOrDefaultAsync(x => x.PatientId == appointment.PatientId);
            var physio = await _applicationDbContext.PhysiotherapistModel.SingleOrDefaultAsync(x => x.PhysiotherapistId == appointment.PhysioTimeSlotsData.PhysiotherapistId);

            var distance = calcCrow(double.Parse(patient.Latitude), double.Parse(patient.Longitude), double.Parse(physio.Latitude), double.Parse(physio.Longitude));
            var amount = 0;
            if (distance < 5)
            {
                amount = 50;
            }
            else
            {
                amount = 100;
            }
            var paymentsModel = new PaymentsModel
            {
                AppointmentId = appointmentId,
                PaymentTypeId = 1,
                Amount = (int)physio.ConsultationCharge,
                DistanceAmount = amount,
                PStatusCode = "1" //unpaid
            };
            _applicationDbContext.Add(paymentsModel);
            await _applicationDbContext.SaveChangesAsync();

            //return RedirectToAction(nameof(HomeController.PhysioAppointmentTable),"Home");
            return Json(new { result = true, msg = "Success" });
        }

    }
}
