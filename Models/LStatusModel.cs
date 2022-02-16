using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class LStatusModel
    {
        [Key]
        public string LStatusModelid { get; set; }
        public string Name { get; set; }
    }
}
