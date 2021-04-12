using System.Collections.Generic;
using GigBusiness.Models.CryptoCompare;

namespace GigBusiness.Contract
{
    public interface ICryptoCompare
    {
        List<Investment> GetInvestments(CryptoCompareSettings settings);
    }
}