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

        public int age { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public string ContactNo { get; set; }
        public string LicenseNo { get; set; }

        public int Experience { get; set; }

        public string CitizenshipNumber { get; set; }

        [ForeignKey(nameof(GenderData))]
        public int GenderId { get; set; }
        public GenderModel GenderData { get; set; }

        [ForeignKey(nameof(UserData))]
        public string UserId { get; set; }
        public ApplicationUser UserData { get; set; }

        public IEnumerable<PhysioCategoryModel> physioCategoryModels { get; set; }//1 to many

        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public IEnumerable<PhysioImage> PhysioImages { get; set; }//1 to many\

        [ForeignKey(nameof(CStatusData))]
        public string CStatusModel { get; set; }
        public virtual CStatusModel CStatusData { get; set; }

        [ForeignKey(nameof(LStatusData))]
        public string LStatusModel { get; set; }
        public virtual LStatusModel LStatusData { get; set; }

    }
    //public class PhysiotherapistViewModel
    //{
    //    public int PhysiotherapistId { get; set; }
    //    [Required]
    //    public string Name1 { get; set; }
    //    public string Address { get; set; }
    //    public string Qualification { get; set; }
    //    public string ContactNo { get; set; }
    //    public string LicenseNo { get; set; }

    //    public string CitizenshipNumber { get; set; }

    //    public int GenderId { get; set; }
    //    public int CategoryId { get; set; }
    //    public GenderModel GenderData { get; set; }

    //    public string UserId { get; set; }
    //    public ApplicationUser UserData { get; set; }
    //}
}
