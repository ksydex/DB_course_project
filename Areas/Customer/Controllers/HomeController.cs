using System;
using ContractAndProjectManager.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractAndProjectManager.Areas.Customer.Controllers
{
    [Authorize(Roles = Role.Keys.Customer)]
    [Area("Customer")]
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