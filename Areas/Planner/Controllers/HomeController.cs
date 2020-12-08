using System;
using ContractAndProjectManager.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractAndProjectManager.Areas.Planner.Controllers
{
    [Authorize(Roles = Role.Keys.Planner )]
    [Area("Planner")]
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