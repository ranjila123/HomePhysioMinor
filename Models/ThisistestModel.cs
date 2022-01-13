using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class ThisistestModel
    {
        public int Id { get; set; }
        [Required ]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
