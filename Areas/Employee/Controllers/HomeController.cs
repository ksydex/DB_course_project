using System;
using ContractAndProjectManager.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractAndProjectManager.Areas.Employee.Controllers
{
    [Authorize(Roles = Role.Keys.Employee )]
    [Area("Employee")]
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