using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreddoIndex.Models
{
    public class CurrencyHistory
    {
        public CurrencyHistory()
        {
            currency = "";
            until = DateTime.Now;
            history = new Dictionary<DateTime, double>();
        }
        public string currency { get; set; }
        public DateTime until { get; set; }
        public Dictionary<DateTime, double> history { get; set; }
    }
}
