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

namespace ContractAndProjectManager.Areas.Project.Controllers
{
    [Authorize(Roles = Role.Keys.TeamLead)]
    [Area("Project")]
    public class ProjectStageController : Controller
    {
        private readonly ApplicationContext _context;

        public ProjectStageController(ApplicationContext context)
        {
            _context = context;
        }

        private IActionResult RedirectToProjectById(int id) =>
            RedirectToAction("Edit", "Project", new { Area = "Project", id });
        
        // GET: ProjectStage
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.ProjectStages.Include(p => p.ContractStage).Include(p => p.Executor).Include(p => p.Project);
            return View(await applicationContext.ToListAsync());
        }

        // GET: ProjectStage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectStage = await _context.ProjectStages
                .Include(p => p.ContractStage)
                .Include(p => p.Executor)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectStage == null)
            {
                return NotFound();
            }

            return View(projectStage);
        }

        // GET: ProjectStage/Create
        public async Task<IActionResult> Create([Required] int contractStageId)
        {
            var contractStage = await _context.ContractStages.FindAsync(contractStageId);
            if (contractStage == null)
                return NotFound();
            
            ViewBag.ContractStage = contractStage;
            
            ViewData["ExecutorId"] = new SelectList(contractStage?.Contract.Project.Team.Employees, "Id", "Name");
            return View();
        }

        // POST: ProjectStage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectStage projectStage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectStage);
                await _context.SaveChangesAsync();
                return RedirectToProjectById(projectStage.ProjectId);
            }

            ViewData["ExecutorId"] = new SelectList(_context.Employees, "Id", "Name", projectStage.ExecutorId);
            ViewData["ContractStageId"] = new SelectList(_context.ContractStages, "Id", "Title", projectStage.ContractStageId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Title", projectStage.ProjectId);
            return View(projectStage);
        }

        // GET: ProjectStage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectStage = await _context.ProjectStages.FindAsync(id);
            if (projectStage == null)
            {
                return NotFound();
            }
            
            ViewData["ContractStageId"] = new SelectList(projectStage.ContractStage.Contract.Stages, "Id", "Title", projectStage.ContractStageId);
            ViewData["ExecutorId"] = new SelectList(projectStage.ContractStage.Contract.Project.Team.Employees, "Id", "Name", projectStage.ExecutorId);
            return View(projectStage);
        }

        // POST: ProjectStage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectStage projectStage)
        {
            if (id != projectStage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectStage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectStageExists(projectStage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToProjectById(projectStage.ProjectId);
            }
            ViewData["ContractStageId"] = new SelectList(_context.ContractStages, "Id", "Id", projectStage.ContractStageId);
            ViewData["ExecutorId"] = new SelectList(_context.Employees, "Id", "Discriminator", projectStage.ExecutorId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", projectStage.ProjectId);
            return View(projectStage);
        }

        // GET: ProjectStage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectStage = await _context.ProjectStages
                .Include(p => p.ContractStage)
                .Include(p => p.Executor)
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (projectStage == null)
            {
                return NotFound();
            }

            return View(projectStage);
        }

        // POST: ProjectStage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectStage = await _context.ProjectStages.FindAsync(id);
            if (projectStage == null)
                return NotFound();
            var projectId = projectStage.ProjectId;
            _context.ProjectStages.Remove(projectStage);
            await _context.SaveChangesAsync();
            return RedirectToProjectById(projectId);
        }

        private bool ProjectStageExists(int id)
        {
            return _context.ProjectStages.Any(e => e.Id == id);
        }
    }
}
