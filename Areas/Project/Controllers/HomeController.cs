using System;
using Microsoft.AspNetCore.Mvc;

namespace ContractAndProjectManager.Areas.Project.Controllers
{
    [Area("Project")]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}