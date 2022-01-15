using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.Json;
using FreddoIndex.Data;

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
        private FreddoIndexContext context;
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
        public CurrenciesHistory(FreddoIndexContext _contact)
        {
            context = _contact;
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
        private static async Task<(DateTime, Double)> GetCurrencyForYear(int year, string currency)
        {
            var res = await GetAsync($"https://api.exchangerate.host/{year}-06-01?symbols={currency}&base=GBP");
            var data = JsonSerializer.Deserialize<CurrencyMappings>(res);
            return (new DateTime(year, 6, 1), data.rates.GetValueOrDefault(currency, 0));
        }
        private static Dictionary<DateTime, double> CombineHistories(
            int from, 
            Dictionary<DateTime, double> cached, 
            Dictionary<DateTime, double> newDates
        )
        {
            return Enumerable.Range(from, DateTime.Now.Year - from)
                .ToList()
                .ToDictionary(year => new DateTime(year, 6, 1), year =>
                {
                    var date = new DateTime(year, 6, 1);
                    return cached.ContainsKey(date) ? cached.GetValueOrDefault(date, 0) : 
                        newDates.GetValueOrDefault(date, 0);
                });
        }
        private async Task<Dictionary<DateTime, double>> GetMissingYear(List<int> missingYears, string currency)
        {
            var changePointsQuery = from changePoint in context.PriceChangePoints
                                    select changePoint;
            var changePointsList = changePointsQuery.ToList();
            var tasks = missingYears.Select(async (year) =>
            {
                (var date, var value) = await GetCurrencyForYear(year, currency);
                var multiplierQuery = from changes in changePointsList
                                      where changes.activeFrom < date && (changes.activeUntil > date || !changes.activeUntil.HasValue)
                                      select changes.price;
                var multiplier = multiplierQuery.FirstOrDefault();
                return (date, multiplier * value);                  
            });
            return (await Task.WhenAll(tasks))
                .ToDictionary(d => d.Item1, d => d.Item2);
        }
        public async Task<CurrencyHistory> GetHistory(string currency, int since)
        {
            if (!acceptedCodes.symbols.ContainsKey(currency))
            {
                throw new Exception("Currency not accepted.");
            }
            var cachedHistory = histories.GetValueOrDefault(currency, new CurrencyHistory());
            var missingYears = Enumerable.Range(since, DateTime.Now.Year - since)
                .Where(i => !cachedHistory.history.ContainsKey(new DateTime(i, 6, 1)))
                .ToList();
            if(missingYears.Count == 0)
            {
                return cachedHistory;
            }
            Dictionary<DateTime, double> results = new Dictionary<DateTime, double>();
            try
            {
                results = await GetMissingYear(missingYears, currency);
            } catch (Exception e)
            {
                throw new Exception($"There was an error querying the history for this currency: {e.Message}");
            }
            var ret = new CurrencyHistory
            {
                history = CombineHistories(since, cachedHistory.history, results),
                currency = currency
            };
            histories[currency] = ret;
            return ret;
        }
    }
}
