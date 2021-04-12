using System.Collections.Generic;
using GigBusiness.Models.CryptoCompare;

namespace GigView.Models
{
    public class IndexViewModel
    {
        public List<Investment> Investments { get; set; }

        public IndexViewModel(List<Investment> investments)
        {
            Investments = investments;
        }
    }
}