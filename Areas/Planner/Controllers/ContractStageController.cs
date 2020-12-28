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

namespace ContractAndProjectManager.Areas.Planner.Controllers
{
    [Authorize(Roles = Role.Keys.Planner )]
    [Area("Planner")]
    public class ContractStageController : Controller
    {
        private readonly ApplicationContext _context;

        private IActionResult ToContract(int id)
        {
            return RedirectToAction("Details", "Contract", new { Area = "Planner", Id = id });
        }

        public ContractStageController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: ContractStage
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.ContractStages.Include(c => c.Contract);
            return View(await applicationContext.ToListAsync());
        }

        // GET: ContractStage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractStage = await _context.ContractStages
                .Include(c => c.Contract)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contractStage == null)
            {
                return NotFound();
            }

            return View(contractStage);
        }

        // GET: ContractStage/Create
        public IActionResult Create([FromQuery] int contractId = default)
        {
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Title", contractId);
            ViewData["ContractIdSet"] = contractId != default;
            return View();
        }

        // POST: ContractStage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DateStart,DateDeadLine,ContractId")] ContractStage contractStage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contractStage);
                await _context.SaveChangesAsync();
                return ToContract(contractStage.ContractId);
            }
            return View(contractStage);
        }

        // GET: ContractStage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractStage = await _context.ContractStages.FindAsync(id);
            if (contractStage == null)
            {
                return NotFound();
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Title", contractStage.ContractId);
            return View(contractStage);
        }

        // POST: ContractStage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DateCreated,DateStart,DateDeadLine,DateEnd,ContractId")] ContractStage contractStage)
        {
            if (id != contractStage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractStage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractStageExists(contractStage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContractId"] = new SelectList(_context.Contracts, "Id", "Title", contractStage.ContractId);
            return View(contractStage);
        }

        // GET: ContractStage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractStage = await _context.ContractStages
                .Include(c => c.Contract)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contractStage == null)
            {
                return NotFound();
            }

            return View(contractStage);
        }

        // POST: ContractStage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contractStage = await _context.ContractStages.FindAsync(id);
            var contractId = contractStage.ContractId;
            _context.ContractStages.Remove(contractStage);
            await _context.SaveChangesAsync();
            return ToContract(contractId);
        }

        private bool ContractStageExists(int id)
        {
            return _context.ContractStages.Any(e => e.Id == id);
        }
    }
}
