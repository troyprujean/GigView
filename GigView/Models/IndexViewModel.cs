using System;
using System.Collections.Generic;
using System.Linq;
using GigBusiness.Models.CryptoCompare;

namespace GigView.Models
{
    public class IndexViewModel
    {
        public List<Investment> Investments { get; set; }

        public double TotalProfit => Math.Round(Investments.Sum(x => x.Profit), 5);
        public double TotalInvestment => Investments.Sum(x => x.InvestmentAmount);
        public double TotalValue => Math.Round(Investments.Sum(x => x.CurrentValue), 5);

        public bool Profitable => TotalValue > TotalInvestment;

        public IndexViewModel(List<Investment> investments)
        {
            Investments = investments;
        }
    }
}