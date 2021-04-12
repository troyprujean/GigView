using System;
using GigBusiness;
using GigBusiness.Models.CryptoCompare;
using GigView.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GigView.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<CryptoCompareSettings> _settings;

        public HomeController(IOptions<CryptoCompareSettings> settings)
        {
            _settings = settings;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var results = new CryptoCompare().GetInvestments(_settings.Value);

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