using System;
using System.Threading.Tasks;
using ContractAndProjectManager.Data;
using ContractAndProjectManager.Entities;
using ContractAndProjectManager.Models;
using ContractAndProjectManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractAndProjectManager.Areas.TeamLead.Controllers
{
    [Authorize(Roles = Role.Keys.TeamLead)]
    [Area("TeamLead")]
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserService _userService;
        
        public HomeController(UserService userService, ApplicationContext context)
        {
            _userService = userService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Team()
        {
            return View(_userService.User.Team);
        }

        public async Task<IActionResult> Contract(int id)
        {
            var contract = await _context.Contracts.FirstOrDefaultAsync(x => x.Id == id);
            if (contract.Project?.TeamId != _userService.User.TeamId)
                return NotFound();

            return View(contract);
        }
    }
}