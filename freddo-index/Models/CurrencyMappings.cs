using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreddoIndex.Models
{
    public class CurrencyMappings
    {
        public CurrencyMappings()
        {
            date = new DateTime();
            rates = new Dictionary<string, double>();
        }
        public DateTime date { get; set; }
        public Dictionary<string, double> rates { get; set; }
    }
}
