using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreddoIndex.Models
{
    public interface ICurrenciesHistory
    {
        public Dictionary<string, CurrencyHistory> histories { get; set; }
        public Task<CurrencyHistory> GetHistory(string currency, int since);
    }
}
