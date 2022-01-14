using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class PaymentTypeModel
    {
        [Key]
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
    }
}
