using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class CStatusModel
    {
        [Key]
        public string CStatusModelId { get; set; }

        public string Name { get; set; }
    }
}
