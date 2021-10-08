using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using freddo_index.Models;
using freddo_index.Data;

namespace freddo_index.Controllers
{
    //[Route("api/[controller]")]
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

        [HttpGet]
        [ActionName("History")]
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
