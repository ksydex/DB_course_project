using System;
using ContractAndProjectManager.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContractAndProjectManager.Areas.TeamLead.Controllers
{
    [Authorize(Roles = Role.Keys.TeamLead)]
    [Area("TeamLead")]
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