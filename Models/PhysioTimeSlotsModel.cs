using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class PhysioTimeSlotsModel
    {
        [Key]
        public int PhysioTimeSlotsId { get; set; }
        
        public int Date { get; set; }
        public int TimeShift { get; set; }

        [ForeignKey(nameof(PhysiotherapistData))]
        public int PhysiotherapistId { get; set; }

        public PhysiotherapistModel PhysiotherapistData { get; set; }
    }
}
