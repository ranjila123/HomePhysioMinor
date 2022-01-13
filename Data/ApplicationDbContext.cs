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




    }
}
