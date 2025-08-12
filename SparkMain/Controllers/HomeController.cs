using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SparkMain.Data;
using SparkMain.Models;
using System.Diagnostics;

namespace SparkMain.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SparkContext _context;

        public HomeController(ILogger<HomeController> logger, SparkContext context)
        {
            _logger = logger;
            this._context = context;
        }

        public IActionResult Index()
        {
            List<BestAndTrend> bestandtrend = new List<BestAndTrend> { new BestAndTrend(_context.TrendingSellings.ToList(), _context.Prouducts.ToList()) };


            return View(bestandtrend);
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Boots()
        {
            var boots = _context.Boots.ToList();

            return View(boots);
        }

        public IActionResult Sports()
        {

            var sports = _context.Sports.ToList();

            return View(sports);

        }

        public IActionResult Oxfords()
        {
            var oxfords = _context.Oxfords.ToList();

            return View(oxfords);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
