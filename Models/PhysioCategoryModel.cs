using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.Models
{
    public class PhysioCategoryModel
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(Physiotherapist))]
        public int PhysiotherapistId { get; set; }

        public PhysiotherapistModel Physiotherapist { get; set; }
        public CategoryModel Category { get; set; }

        public string Experience { get; set; }
    }
}
