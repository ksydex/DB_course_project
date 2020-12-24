using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContractAndProjectManager.Data;
using ContractAndProjectManager.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;
using Task = ContractAndProjectManager.Entities.Task;
using TaskStatus = ContractAndProjectManager.Entities.TaskStatus;

namespace ContractAndProjectManager.Areas.Project.Controllers
{
    [Authorize(Roles = Role.Keys.TeamLead)]
    [Area("Project")]
    public class TaskController : Controller
    {
        private readonly ApplicationContext _context;

        public TaskController(ApplicationContext context)
        {
            _context = context;
        }
        
        private IActionResult RedirectToProjectById(int id) =>
            RedirectToAction("Edit", "Project", new { Area = "TeamLead", id });

        // GET: Task
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Tasks.Include(t => t.Executor).Include(t => t.Stage);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Task/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Executor)
                .Include(t => t.Stage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Task/Create
        public async Task<IActionResult> Create([Required] int projectStageId)
        {
            var projectStage = await _context.ProjectStages.FindAsync(projectStageId);
            if (projectStage == null)
                return NotFound();
            
            ViewBag.ProjectStage = projectStage;
            
            ViewData["ExecutorId"] = new SelectList(projectStage.Project.Team.Employees, "Id", "Name");

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToProjectById((await _context.ProjectStages.FindAsync(task.StageId)).ProjectId);
            }
            ViewData["ExecutorId"] = new SelectList((await _context.ProjectStages.FindAsync(task.StageId)).Project.Team.Employees, "Id", "Name", task.ExecutorId);
            ViewData["StageId"] = new SelectList(_context.ProjectStages, "Id", "Id", task.StageId);
            return View(task);
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["ExecutorId"] = new SelectList(task.Stage.Project.Team.Employees, "Id", "Name", task.ExecutorId);
            ViewData["StageId"] = new SelectList(task.Stage.Project.Stages, "Id", "Title", task.StageId);
            return View(task);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToProjectById((await _context.ProjectStages.FindAsync(task.StageId)).ProjectId);
            }
            ViewData["ExecutorId"] = new SelectList(_context.Employees, "Id", "Discriminator", task.ExecutorId);
            ViewData["StageId"] = new SelectList(_context.ProjectStages, "Id", "Id", task.StageId);
            return View(task);
        }

        // GET: Task/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Executor)
                .Include(t => t.Stage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            var projectId = (await _context.ProjectStages.FindAsync(task.StageId)).ProjectId;
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToProjectById(projectId);
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
