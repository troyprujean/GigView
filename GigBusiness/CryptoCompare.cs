using System;
using GigBusiness.Models.CryptoCompare;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GigBusiness.Contract;
using Newtonsoft.Json;

namespace GigBusiness
{
    public class CryptoCompare : ICryptoCompare
    {
        private const string TokenName = "Apikey";

        public List<Investment> GetInvestments(CryptoCompareSettings settings)
        {
            var multipleSymbolsUrl = BuildMultipleSymbolsUrl(settings.GetSymbols);
            
            var client = GetHttpClient(settings);

            var response = client.GetAsync(multipleSymbolsUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                client.Dispose();
                var results = GetSymbolToValueDictionary(JsonConvert.DeserializeObject<MultipleSymbols>(result));
                return MapResultsToInvestments(results, settings);
            }

            var errorMessage = response.Content.ReadAsStringAsync().Result;
            client.Dispose();

            throw new Exception(errorMessage);
        }

        private static List<Investment> MapResultsToInvestments(IReadOnlyDictionary<string, double> results, CryptoCompareSettings settings)
        {
            var investments = new List<Investment>();

            foreach (var symbol in settings.Coins)
            {
                if (!results.ContainsKey(symbol.Symbol))
                    throw new Exception($"Symbol: {symbol.Symbol} not implemented on MultipleSymbols Class");

                var investment = new Investment
                {
                    Symbol = symbol.Symbol,
                    CurrentPrice = results[symbol.Symbol], 
                    InvestmentAmount = symbol.Investment,
                    Count = symbol.Count
                };

                investments.Add(investment);
            }

            return investments;
        }

        private static Dictionary<string, double> GetSymbolToValueDictionary(MultipleSymbols results)
        {
            return results.GetType()
                .GetProperties()
                .Where(x => x.GetValue(results) != null)
                .Select(x => new
                {
                    x.Name,
                    Symbol = (Symbol)x.GetValue(results)
                })
                .ToDictionary(x => x.Name, x => x.Symbol.NZD);
        }

        private static string BuildMultipleSymbolsUrl(IEnumerable<string> symbols)
        {
            return $"pricemulti?fsyms={string.Join(",", symbols)}&tsyms=NZD";
        }

        private static HttpClient GetHttpClient(CryptoCompareSettings settings)
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(settings.Url) };
            httpClient.DefaultRequestHeaders.Add(TokenName, settings.ApiKey);
            return httpClient;
        }

    }
}