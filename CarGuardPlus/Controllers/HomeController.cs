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
        private readonly IMyLicencesService _myLicencesService;
        private readonly SendAlertViewModel _sendAlertViewModel;

        public HomeController(ILogger<HomeController> logger,
            ISendAlertService sendAlertService,
            IMyAlertService myAlertService,
            IMyLicencesService myLicencesService,
            SendAlertViewModel sendAlertViewModel)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sendAlertService = sendAlertService;
            _myAlertService = myAlertService;
            _myLicencesService = myLicencesService;
            _sendAlertViewModel = sendAlertViewModel;
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
            if (_sendAlertService.GetLicence(licence) is null)
            {
                _sendAlertViewModel.LicenceIsValid = 1;
                return View(_sendAlertViewModel);
            }
            _sendAlertViewModel.LicenceIsValid = 2;
            await _sendAlertService.SendAlert(licence, message);
            return SendAlert();
        }
        public IActionResult SendAlert()
        {
            return View(_sendAlertViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddLicenceNumber(string licence)
        {
            if (_myLicencesService.LicenceAlreadyExist(licence) == true)
            {
                AddLicenceNumberViewModel viewModel = new();
                viewModel.LicenceAlreadyExist = true;
                viewModel.ListOfLicences = _myLicencesService.GetLicences();
                return View(viewModel);
            }
            if (licence is not null)
            {
               await _myLicencesService.AddLicencePlate(licence);
            }
            return AddLicenceNumber();
        }
        public IActionResult AddLicenceNumber()
        {
            AddLicenceNumberViewModel addLicenceNumberViewModel = new AddLicenceNumberViewModel
            {
                ListOfLicences = _myLicencesService.GetLicences()
            };
            return View(addLicenceNumberViewModel);
        }
        [HttpGet]
        public IActionResult MyAlerts()
        {
            var alerts = _myAlertService.GetAlerts();
            return View(alerts);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteLicence(string licence)
        {
            await _myLicencesService.DeleteLicencePlate(licence);
            return RedirectToAction("AddLicenceNumber");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}