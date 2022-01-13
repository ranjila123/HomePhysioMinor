using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class GenderModel
    {
        [Key]
        public int GenderId { get; set; }
        public string TypeName { get; set; }
        public IEnumerable<PhysiotherapistModel> Physiotherapists { get; set; }
    }
}
