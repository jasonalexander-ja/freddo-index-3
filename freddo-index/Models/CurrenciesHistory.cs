using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.Json;

namespace FreddoIndex.Models
{
    public class LanguageCode
    {
        public Dictionary<string, Dictionary<string, string>> symbols { get; set; }
    }
    public class CurrenciesHistory : ICurrenciesHistory
    {
        public Dictionary<string, CurrencyHistory> histories { get; set; } = new Dictionary<string, CurrencyHistory>();
        public LanguageCode acceptedCodes { get; set; } = new LanguageCode();
        private static async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            try
            {
                using (var response = (HttpWebResponse)await request.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string value = await reader.ReadToEndAsync();
                    return value;
                }
            } catch(Exception e)
            {
                throw new Exception($"Failed to get a reponse from currency convertyer service: {e.Message}");
            }
        }
        public CurrenciesHistory()
        {
            Task.Run(async () =>
            {
                try
                {
                    var res = await GetAsync("https://api.exchangerate.host/symbols");
                    var s = JsonSerializer.Deserialize<LanguageCode>(res);
                    acceptedCodes.symbols = s.symbols;
                }
                catch (Exception e)
                {
                }
            }).GetAwaiter().GetResult();
        }
        public async Task<CurrencyHistory> GetHistory(string currency, int since)
        {
            int currentYear = DateTime.Now.Year;
            if (!acceptedCodes.symbols.ContainsKey(currency))
            {
                throw new Exception("Currency not accepted.");
            }
            if (histories.ContainsKey(currency))
            {
                var cachedRet = histories.GetValueOrDefault(currency);
                if (cachedRet.until.Year == currentYear) {
                    return histories.GetValueOrDefault(currency);
                }
            }
            var urls = Enumerable.Range(since, currentYear - since)
                .ToList()
                .Select(i => ($"https://api.exchangerate.host/{i}-06-01?symbols={currency}&base=GBP", i));
            try
            {
                var tasks = urls.Select(async i =>
                {
                    (string url, int date) = i;
                    var res = await GetAsync(url);
                    var data = JsonSerializer.Deserialize<CurrencyMappings>(res);
                    double value = data.rates.GetValueOrDefault(currency);
                    return (new DateTime(date, 6, 1), value);
                }).ToArray();
                var results = (await Task.WhenAll(tasks))
                    .ToDictionary(d => d.Item1, d => d.Item2);
                var ret = new CurrencyHistory
                {
                    history = results,
                    currency = currency,
                    until = DateTime.Now
                };
                histories.Add(currency, ret);
                return ret;
            } catch(Exception e)
            {
                throw new Exception($"Failed to get history for the currency: {e.Message}");
            }
        }
    }
}
