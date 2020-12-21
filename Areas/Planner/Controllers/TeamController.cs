using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContractAndProjectManager.Data;
using ContractAndProjectManager.Entities;
using Microsoft.AspNetCore.Authorization;
using Task = System.Threading.Tasks.Task;

namespace ContractAndProjectManager.Areas.Planner.Controllers
{
    [Authorize(Roles = Role.Keys.Planner)]
    [Area("Planner")]
    public class TeamController : Controller
    {
        private readonly ApplicationContext _context;

        public TeamController(ApplicationContext context)
        {
            _context = context;
        }

        private IActionResult RedirectToEditById(int id) => RedirectToAction("Edit", new { id });

        public async Task<IActionResult> AddEmployee(int employeeId, int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            var employee = await _context.Employees.FindAsync(employeeId);

            if (team == null || employee == null)
                return BadRequest();

            employee.TeamId = team.Id;
            await _context.SaveChangesAsync();

            return RedirectToEditById(team.Id);
        }
        
        public async Task<IActionResult> RemoveEmployee(int employeeId, int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            var employee = await _context.Employees.FindAsync(employeeId);

            if (team == null || employee == null)
                return BadRequest();

            employee.TeamId = null;
            await _context.SaveChangesAsync();

            return RedirectToEditById(team.Id);
        }

        // GET: Team
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.ToListAsync());
        }

        // GET: Team/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Team/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team, int teamLeadId)
        {
            if (ModelState.IsValid)
            {
                var teamLead = await _context.TeamLeads.FindAsync(teamLeadId);
                if (teamLead == null)
                    return View(team);
                
                _context.Add(team);
                await _context.SaveChangesAsync();
                
                teamLead.TeamId = team.Id;
                await _context.SaveChangesAsync();
                
                return RedirectToEditById(team.Id);
            }
            return View(team);
        }

        // GET: Team/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return View(team);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Team team, int teamLeadId)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var teamLead = await _context.TeamLeads.FindAsync(teamLeadId);
                    if (teamLead == null)
                        return View(team);
                    
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                    
                    var currentTeamLead = await _context.TeamLeads.FirstOrDefaultAsync(x => x.TeamId == team.Id);
                    if(currentTeamLead != null)
                        currentTeamLead.TeamId = null;

                    teamLead.TeamId = team.Id;
                    await _context.SaveChangesAsync();
                    return RedirectToEditById(team.Id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(team);
        }

        // GET: Team/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
