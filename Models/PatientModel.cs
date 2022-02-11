    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class PatientModel
    {
        [Key]
        public int PatientId { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        [ForeignKey(nameof(GenderData))]
        public int GenderId { get; set; }
        public GenderModel GenderData { get; set; }

        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }

        [ForeignKey(nameof(UserData))]
        public string UserId { get; set; }
        public ApplicationUser UserData { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}
