using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.Json;

namespace FreddoIndex.Models
{
    public class CurrenciesHistory : ICurrenciesHistory
    {
        public Dictionary<string, CurrencyHistory> histories { get; set; } = new Dictionary<string, CurrencyHistory>();

        private class CurrencyMappingsRes : CurrencyMappings
        {

        }
        static async Task<string> GetHistoryForCurrencyAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string value = await reader.ReadToEndAsync();
                return value;
            }
        }
        public async Task<CurrencyHistory> GetHistory(string currency, int since)
        {
            int currentYear = DateTime.Now.Year;
            if (histories.ContainsKey(currency))
            {
                var cached_ret = new CurrencyHistory();
                histories.TryGetValue(currency, out cached_ret);
                if (cached_ret.until.Year == currentYear) {
                    return cached_ret;
                }
            }
            var urls = new List<(string, int)>();
            for(int i = since; i < currentYear; i++)
            {
                urls.Add(($"https://api.exchangerate.host/{i}-06-01?symbols={currency}&base=GBP", i));
            }
            var results = new List<(DateTime, double)>();
            Task.WaitAll(urls.Select(async i => {
                (string url, int date) = i;
                var res = await GetHistoryForCurrencyAsync(url);
                var data = JsonSerializer.Deserialize<CurrencyMappings>(res);
                double value = 0;
                data.rates.TryGetValue(currency, out value);
                if (value != 0)
                {
                    results.Add((new DateTime(date, 6, 1), value));
                }
            }).ToArray());
            var ret = new CurrencyHistory();
            foreach((DateTime, double) val in results) {
                (DateTime date, double value) = val;
                ret.history.Add(date, value);
            }
            ret.currency = currency;
            ret.until = DateTime.Now;
            histories.Add(currency, ret);
            return ret;
        }
    }
}
