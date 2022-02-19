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
           


        [Key]
        
        public int PaymentsId { get; set; }

        [ForeignKey(nameof(AppointmentsData))]
        public int AppointmentId { get; set; }
        public AppointmentsModel AppointmentsData { get; set; }

        [ForeignKey(nameof(PaymentTypeData))]
        public int PaymentTypeId { get; set; }
        public PaymentTypeModel PaymentTypeData { get; set; }

        public int Amount { get; set; }

        public int DistanceAmount { get; set; }

        [ForeignKey(nameof(PStatusData))]
        public string PStatusCode { get; set; }

        public PStatusModel PStatusData { get; set; }
    }
}
