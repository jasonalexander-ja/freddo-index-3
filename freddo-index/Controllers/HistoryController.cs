using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using FreddoIndex.Models;
using FreddoIndex.Data;

namespace FreddoIndex.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private ICurrencyMaps maps;
        private FreddoIndexContext context;
        public HistoryController(ICurrencyMaps _maps, FreddoIndexContext _context)
        {
            maps = _maps;
            context = _context;
        }

        [HttpGet("{date}")]
        public async Task<CurrencyMappings> HistoryGet(DateTime date)
        {
            var ret = new CurrencyMappings();
            var changePointsListQuery = from changePoints in context.PriceChangePoints
                                        where date > changePoints.activeFrom && date < changePoints.activeUntil
                                        select changePoints;
            var changePoint = changePointsListQuery.FirstOrDefault();
            var currencyMappings = await maps.GetMapFor(date);
            foreach (var item in currencyMappings.rates)
            {
                double newVal = item.Value * changePoint.price;
                ret.rates.Add(item.Key, newVal);
            }
            return ret;
        }
    }
}
