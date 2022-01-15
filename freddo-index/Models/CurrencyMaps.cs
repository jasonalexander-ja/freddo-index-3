using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace FreddoIndex.Models
{
    public class CurrencyMaps: ICurrencyMaps
    {
        private static readonly CurrencyMaps _currencyMappingsServiceInstance = new CurrencyMaps();
        public static CurrencyMaps GetInstance() => _currencyMappingsServiceInstance;
        public Dictionary<DateTime, CurrencyMappings> maps { get; set; } = new Dictionary<DateTime, CurrencyMappings>();
        private static async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)await request.GetResponseAsync();
            } catch(Exception e)
            {
                throw new Exception($"Failed to get a response from currency converter service: {e.Message}");
            }
            using (response)
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
        public async Task<CurrencyMappings> GetMapFor(DateTime date)
        {
            if (maps.ContainsKey(date))
            {
                return maps.GetValueOrDefault(date);
            }
            string url = $"https://api.exchangerate.host/{date.ToString("yyyy-MM-dd")}?base=GBP";
            string res = await GetAsync(url);
            CurrencyMappings ret = new CurrencyMappings();
            try
            {
                ret = JsonSerializer.Deserialize<CurrencyMappings>(res);
            } catch(Exception e)
            {
                throw new Exception($"Couldn't get the currency mappings: {e.Message}");
            }
            maps.Add(date, ret);
            return ret;
        }
    }
}
