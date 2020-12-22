using System;
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
        
        [HttpPost]
        public async Task<IActionResult> SetStatus(int projectId, int statusId)
        {
            var project = await _context.Projects.FindAsync(projectId);
            var status = await _context.ProjectStatuses.FindAsync(statusId);
            if (project == null || status == null || project.TeamId != _userService.User.TeamId)
                return BadRequest();

            await _context.ProjectStatusHistories.AddAsync(new ProjectStatusHistory
            {
                EntityId = project.Id,
                StatusId = status.Id
            });

            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", "Project", new {Area = "TeamLead", id = project.Id});
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