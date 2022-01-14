using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class PStatusModel
    {
        [Key]
        public string PStatuCode { get; set; }
        public string PStatusType { get; set; }
    }
}
