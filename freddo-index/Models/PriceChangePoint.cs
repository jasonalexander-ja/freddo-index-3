using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace freddo_index.Models
{
    public class PriceChangePoint
    {
        public PriceChangePoint()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public double price { get; set; }
        public DateTime activeFrom { get; set; }
        public DateTime activeUntil { get; set; }
    }
}
