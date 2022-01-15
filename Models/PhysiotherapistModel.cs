using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class PhysiotherapistModel
    {
        [Key]
        public int PhysiotherapistId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public string ContactNo { get; set; }
        public string LicenseNo { get; set; }
        [ForeignKey(nameof(GenderData))]
        public int GenderId { get; set; }
        public GenderModel GenderData { get; set; }

        [ForeignKey(nameof(UserData))]
        public string UserId { get; set; }
        public ApplicationUser UserData { get; set; }


    }
}
