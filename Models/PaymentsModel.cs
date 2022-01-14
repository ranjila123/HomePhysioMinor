using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class PaymentsModel
    {
        //Startup garna aagadi update database garera hera ani table haru aauxa hai maile banako wala 

        //yeta update garna khojxada error aako k dai lai sodera gara la update aru sabai ko garisake 
        //   ApplicationDbContext ma dbset haru ni lekhisake hera la 
           


        [Key, ForeignKey(nameof(PaymentTypeData))]
        public int PaymentsId { get; set; }
        public PaymentTypeModel PaymentTypeData { get; set; }


        [ForeignKey(nameof(PhysioTimeSlotsData))]
        public int PhysioTimeSlotsId { get; set; }
        public PhysioTimeSlotsModel PhysioTimeSlotsData { get; set; }


        [ForeignKey(nameof(PatientData))]
        public int PatientId { get; set; }
        public PatientModel PatientData { get; set; }

        public int Amount { get; set; }
        public int DistanceAmount { get; set; }

        [ForeignKey(nameof(PStatusData))]
        public string PStatusCode { get; set; }

       // public PStatusModel PStatusData { get; set; }
    }
}
