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

namespace ContractAndProjectManager.Areas.Project.Controllers
{
    [Authorize(Roles = Role.Keys.TeamLead + "," + Role.Keys.Employee + "," + Role.Keys.Planner)]
    [Area("Project")]
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
        [Authorize(Roles = Role.Keys.TeamLead)]
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

            return RedirectToAction("Edit", "Project", new { Area = "Project", id = project.Id });
        }

        public async Task<IActionResult> Index()
        {
            var projects = await (_userService.User.RoleId == Role.Planner.Id
                ? _context.Projects.Where(x =>
                    (_userService.User as Entities.Planner).Contracts.Select(y => y.Id).Contains(x.ContractId))
                : _context.Projects.Where(x => x.TeamId == _userService.User.TeamId)).ToListAsync();
            return View(projects);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (_userService.User.RoleId != Role.Planner.Id && project.TeamId != _userService.User.TeamId)
                return NotFound();

            return View(project);
        }
    }
}