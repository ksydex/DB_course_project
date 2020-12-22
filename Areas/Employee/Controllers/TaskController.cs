using System;
using System.Threading.Tasks;
using ContractAndProjectManager.Data;
using ContractAndProjectManager.Entities;
using ContractAndProjectManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskStatus = ContractAndProjectManager.Entities.TaskStatus;

namespace ContractAndProjectManager.Areas.Employee.Controllers
{
    [Authorize(Roles = Role.Keys.Employee )]
    [Area("Employee")]
    public class TaskController: Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserService _userService;

        public TaskController(ApplicationContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        private IActionResult ReturnToHome() => RedirectToAction("Index", "Home", new { Area = "Employee" });

        [HttpPost]
        public async Task<IActionResult> SetStatus(int taskId, int statusId, bool returnToHome = false)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            var status = await _context.TaskStatuses.FindAsync(statusId);
            if (task == null || status == null || task.ExecutorId != _userService.UserId || task.Stage.Project.TeamId != _userService.User.TeamId)
                return BadRequest();

            await _context.TaskStatusHistories.AddAsync(new TaskStatusHistory
            {
                EntityId = task.Id,
                StatusId = status.Id
            });

            if (status.Id == TaskStatus.Completed.Id)
                task.DateEnd = DateTime.Now;
            else
                task.DateEnd = null;

            await _context.SaveChangesAsync();

            return returnToHome ? ReturnToHome() : Ok(); // change
        }
    }
}