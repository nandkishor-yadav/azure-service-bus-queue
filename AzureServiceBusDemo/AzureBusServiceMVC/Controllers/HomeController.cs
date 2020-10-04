using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AzureBusServiceMVC.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using AzureBusServiceMVC.Services;

namespace AzureBusServiceMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly private ServiceBusSender _serviceBusSender;

        public HomeController(ILogger<HomeController> logger, ServiceBusSender serviceBusSender)
        {
            _logger = logger;
            _serviceBusSender = serviceBusSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ProducesResponseType(typeof(PayloadForServiceBus), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PayloadForServiceBus), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([Required] PayloadForServiceBus request)
        {
            await _serviceBusSender.SendMessage(new PayloadForServiceBus
            {
                Email = request.Email,
                Message = request.Message
            });

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Privacy()
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
