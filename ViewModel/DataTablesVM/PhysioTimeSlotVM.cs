using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.ViewModel.DataTablesVM
{
    public class PhysioTimeSlotVM
    {
        public int PhysioTimeSlotsId { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
    }
}
