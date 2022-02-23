using HomePhysio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.ViewModel
{
    public class PhysiotherapistVM
    {
        public int PhysiotherapistId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string GenderTypeName { get; set; }

        public int Age { get; set; }

        //public string Email { get; set; }
        public string UserId { get; set; }
        public ApplicationUser UserData { get; set; }

        public string ContactNo { get; set; }
        public string Qualification { get; set; }
        
        public string Category { get; set; }

        public IEnumerable<PhysioCategoryModel> CategoryList { get; set; }
        public string Email { get; set; }
        public decimal ConsultationCharge { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public byte[] PImg { get; set; }

    }
}
