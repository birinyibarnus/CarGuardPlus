using CarGuardPlus.BLL;
using CarGuardPlus.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarGuardPlus.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISendAlertService _sendAlertService;
        private readonly IMyAlertService _myAlertService;

        public HomeController(ILogger<HomeController> logger, ISendAlertService sendAlertService, IMyAlertService myAlertService)
        {
            _logger = logger;
            _sendAlertService = sendAlertService;
            _myAlertService = myAlertService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendAlert(string licence, string message)
        {
            await _sendAlertService.SendAlert(licence, message);
            return RedirectToAction("SendAlert");
        }
        public IActionResult SendAlert()
        {
            return View();
        }
        [HttpGet]
        public IActionResult MyAlerts()
        {
            var alerts = _myAlertService.GetAlerts();
            return View(alerts);
        }

        public IActionResult AddLicenceNumber()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}