using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using FreddoIndex.Models;
using FreddoIndex.Data;

namespace FreddoIndex.Controllers
{
    [Route("Currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private ICurrenciesHistory currencyHistory;
        private FreddoIndexContext context;
        public CurrencyController(ICurrenciesHistory _histories, FreddoIndexContext _context)
        {
            currencyHistory = _histories;
            context = _context;
        }

        [HttpGet("{currency}")]
        public async Task<ActionResult<CurrencyHistory>> CurrencyGet(string currency)
        {
            var ret = new CurrencyHistory();
            var datesQuery = from changePoints in context.PriceChangePoints
                             orderby changePoints.activeFrom ascending
                             select changePoints.activeFrom;
            var fromDate = datesQuery.FirstOrDefault();
            ret = await currencyHistory.GetHistory(currency, fromDate.Year);
            return ret;
        }
    }
}
