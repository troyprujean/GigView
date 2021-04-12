using System.Collections.Generic;
using System.Linq;

namespace GigBusiness.Models.CryptoCompare
{
    public class CryptoCompareSettings
    {
        public string Url { get; set; }
        public string ApiKey { get; set; }
        public CryptoSettingSymbol[] Coins { get; set; }
        public IEnumerable<string> GetSymbols => Coins.Select(x => x.Symbol);
        public double TotalInvestment => Coins.Sum(x => x.Investment);
    }

    public class CryptoSettingSymbol
    {
        public string Symbol { get; set; }
        public double Count { get; set; }
        public double Investment { get; set; }
    }
}