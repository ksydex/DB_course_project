using System.Linq;
using System.Threading.Tasks;
using ContractAndProjectManager.Data;
using ContractAndProjectManager.Entities;
using ContractAndProjectManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace ContractAndProjectManager.Areas.TeamLead.Controllers
{
    [Authorize(Roles = Role.Keys.TeamLead)]
    [Area("TeamLead")]
    public class ProjectController : Controller
    {
        private readonly UserService _userService;
        private readonly ApplicationContext _context;

        public ProjectController(UserService userService, ApplicationContext context)
        {
            _userService = userService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Projects.Where(x => x.TeamId == _userService.User.TeamId).ToListAsync());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project.TeamId != _userService.User.TeamId)
                return NotFound();

            return View(project);
        }
    }
}