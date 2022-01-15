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
        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        [ForeignKey(nameof(GenderData))]
        public int GenderId { get; set; }
        public GenderModel GenderData { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string Address { get; set; }

        [ForeignKey(nameof(UserData))]
        public string UserId { get; set; }
        public ApplicationUser UserData { get; set; }
    }
}
