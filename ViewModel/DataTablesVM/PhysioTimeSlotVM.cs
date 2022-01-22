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

    public class PhysioTimeSlotForAppointmentVM
    {
        public int PhysioTimeSlotsId { get; set; }

        public DateTime DateAndTime { get; set; }
        public string Date { get;  set; }
        public string Time { get; set; }
    }
}
