using HomePhysio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) 
        {

        }
        public DbSet<PhysiotherapistModel> PhysiotherapistModel { get; set; }
        public DbSet<GenderModel> GenderModel { get; set; }
        public DbSet<CategoryModel> CategoryModel { get; set; }
        public DbSet<PatientModel> PatientModel { get; set; }
        public DbSet<ImageTypeModel> ImageTypeModel { get; set; }

        public DbSet<PhysioImage> PhysioImage { get; set; }
        public DbSet<AppointmentsModel> AppointmentsModel { get; set; }
        public DbSet<StatusModel> StatusModel { get; set; }
        public DbSet<PStatusModel> PStatusModel { get; set; }

        public DbSet<PhysioTimeSlotsModel> PhysioTimeSlotsModel { get; set; }

        public DbSet<PaymentTypeModel> PaymentTypeModel { get; set; }
       // public DbSet<PaymentsModel> PaymentsModel { get; set; }






    }
}
