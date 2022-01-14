using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class ImageTypeModel
    {
        [Key]
        public int ImgId { get; set; }
        public string Imgtype { get; set; }

    }
}
