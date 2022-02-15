using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class PatientImage
    {
        public int PatientImageId { get; set; }
        [ForeignKey(nameof(ImageData))]
        public int ImgId { get; set; }
        public ImageTypeModel ImageData { get; set; }

   
        [ForeignKey(nameof(PatientData))]
        public int PatientId { get; set; }
        public PatientModel PatientData { get; set; }

         public byte[] Image { get; set; }
    }
}
