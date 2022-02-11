using HomePhysio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.ViewModel
{
    public class PatientViewModel
    {

       
        public int PatientId { get; set; }
        [Required]
        public string Name1 { get; set; }

        public int Age { get; set; }

        public int GenderId { get; set; }
        public GenderModel GenderData { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string Address { get; set; }

        
        public string UserId { get; set; }
        public ApplicationUser UserData { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
