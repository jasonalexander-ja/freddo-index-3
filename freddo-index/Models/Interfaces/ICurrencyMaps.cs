using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreddoIndex.Models
{
    public interface ICurrencyMaps
    {
        public Dictionary<DateTime, CurrencyMappings> maps { get; set; }
        public Task<CurrencyMappings> GetMapFor(DateTime date);

    }
}
