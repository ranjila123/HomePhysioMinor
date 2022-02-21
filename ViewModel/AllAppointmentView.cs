using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.ViewModel
{
    public class AllAppointmentView
    {
        public int AppointmentId { get; set; }
        public string PhysiotherapistName { get; set; }

        public string PatientName { get; set; }

        public DateTime DateAndTime { get; set; }
        
        public string StatusType { get; set; }
    }
}
