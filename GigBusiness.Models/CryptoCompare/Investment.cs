using System;

namespace GigBusiness.Models.CryptoCompare
{
    public class Investment
    {
        public string Symbol { get; set; }
        public double CurrentPrice { get; set; }
        public double InvestmentAmount { get; set; }
        public double Count { get; set; }

        public double CurrentValue => CurrentPrice * Count;
        public bool Profitable => CurrentValue > InvestmentAmount;
        public double Profit => Math.Round(CurrentValue - InvestmentAmount, 5);
    }
}