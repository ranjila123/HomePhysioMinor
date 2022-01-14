using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class StatusModel
    {
        [Key]
     
        public string StatusCode { get; set; }
        public string StatusType { get; set; }
    }
}
