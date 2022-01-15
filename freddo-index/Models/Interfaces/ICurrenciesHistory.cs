using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreddoIndex.Data;

namespace FreddoIndex.Models
{
    public interface ICurrenciesHistory
    {
        private FreddoIndexContext context;
        public Dictionary<string, CurrencyHistory> histories { get; set; }
        public Task<CurrencyHistory> GetHistory(string currency, int since);
    }
}
