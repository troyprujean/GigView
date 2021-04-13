using System;
using GigBusiness.Contract;
using GigBusiness.Models.CryptoCompare;
using GigView.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GigView.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<CryptoCompareSettings> _settings;
        private readonly ICryptoCompare _cryptoCompare;

        public HomeController(IOptions<CryptoCompareSettings> settings, ICryptoCompare cryptoCompare)
        {
            _settings = settings;
            _cryptoCompare = cryptoCompare;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var results = _cryptoCompare.GetInvestments(_settings.Value);

                return View(new IndexViewModel(results));
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;

                return View("Error", new ErrorViewModel(ex));
            }

        }
    }
}