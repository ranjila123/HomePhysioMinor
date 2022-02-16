using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class CitizenshipTable
    {
        [Key]
        public string CitizenShipNumber { get; set; }
        public string Name { get; set; }
    }
}
