using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace freddo_index.Models
{
    public interface ICurrencyMaps
    {
        public Dictionary<DateTime, CurrencyMappings> maps { get; set; }
        public Task<CurrencyMappings> GetMapFor(DateTime date);

    }
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
    public class CurrencyMaps: ICurrencyMaps
    {
        private static readonly CurrencyMaps _currencyMappingsServiceInstance = new CurrencyMaps();
        public static CurrencyMaps GetInstance() => _currencyMappingsServiceInstance;
        public Dictionary<DateTime, CurrencyMappings> maps { get; set; } = new Dictionary<DateTime, CurrencyMappings>();

        static async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
        public async Task<CurrencyMappings> GetMapFor(DateTime date)
        {
            CurrencyMappings ret = new CurrencyMappings();
            if (maps.ContainsKey(date))
            {
                return maps.GetValueOrDefault(date);
            }
            string url = $"https://api.exchangerate.host/{date.Year}-{date.Month}-{date.Day}?base=GBP";
            string res = await GetAsync(url);
            ret = JsonSerializer.Deserialize<CurrencyMappings>(res);
            maps.Add(date, ret);
            return ret;
        }
    }
}
