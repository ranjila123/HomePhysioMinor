using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class PhysioImage
    {
        [Key]
        [ForeignKey(nameof(ImageData))]
        public int ImgId { get; set; }
        public ImageTypeModel ImageData { get; set; }

   
        [ForeignKey(nameof(PhysiotherapistData))]
        public int PhysiotherapistId { get; set; }
        public PhysiotherapistModel PhysiotherapistData { get; set; }

         public byte[] Image { get; set; }
    }
}
