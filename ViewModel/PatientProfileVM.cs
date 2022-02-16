using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.ViewModel
{
    public class PatientProfileVM
    {
        public int AppointmentId { get; set; }
        
        public DateTime DateAndTime { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public string Status { get; set; }

        public string Physiotherapist { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

    }
}
