using System;
using System.Collections.Generic;
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

        public string ContactNo { get; set; }
        public string Qualification { get; set; }
        
        public string Category { get; set; }

    }
}
