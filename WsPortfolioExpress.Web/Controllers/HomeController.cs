using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using WsPortfolioExpress.Web.Models;
using WsPortfolioExpress.Web.Services;

namespace WsPortfolioExpress.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            GetUserLoginInfo(UserService.UserLogin);
            OnLoadHeaderComponent();
            return View();
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

        private void OnLoadHeaderComponent()
        {
            var pFolder = Path.Combine(_environment.WebRootPath, "settings/");
            string pfilePath = Path.Combine(pFolder, "");
            GetHeaderDataInfo(pfilePath);
        }
    }
}
