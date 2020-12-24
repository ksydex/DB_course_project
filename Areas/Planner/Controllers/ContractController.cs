using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContractAndProjectManager.Data;
using ContractAndProjectManager.Entities;
using ContractAndProjectManager.Services;
using Microsoft.AspNetCore.Authorization;

namespace ContractAndProjectManager.Areas.Planner.Controllers
{
    [Authorize(Roles = Role.Keys.Planner)]
    [Area("Planner")]
    public class ContractController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserService _userService;

        public ContractController(ApplicationContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: Contract
        public async Task<IActionResult> Index([FromQuery] int statusId = 0)
        {

            var applicationContext = _context.Contracts.AsQueryable();
            if (statusId != default && await _context.ContractStatuses.AnyAsync(x => x.Id == statusId))
                applicationContext = applicationContext.Where(x =>
                    x.StatusHistory.OrderByDescending(y => y.Id).Take(1).FirstOrDefault().StatusId == statusId);
            
            ViewData["statusId"] = statusId;
            
            return View(await applicationContext.ToListAsync());
        }

        // GET: Contract/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Planner)
                .Include(c => c.Request)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contract/Create
        public IActionResult Create([FromQuery] int requestId = default)
        {
            var reqs = _context.Requests.Where(x =>
                x.StatusHistory.OrderByDescending(y => y.Id).Take(1).FirstOrDefault().StatusId ==
                RequestStatus.Pending.Id && x.Contract == null).ToList();

            ViewData["RequestId"] = new SelectList(reqs, "Id", "Title", requestId);
            ViewData["RequestIdSet"] = requestId != default;

            return View();
        }

        // POST: Contract/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Title,Description,DateStart,DateDeadLine,RequestId")]
            Contract contract)
        {
            if (ModelState.IsValid)
            {
                contract.PlannerId = _userService.UserId;
                _context.Add(contract);
                await _context.SaveChangesAsync();

                
                await _context.RequestStatusHistories.AddAsync(new RequestStatusHistory
                {
                    EntityId = contract.RequestId,
                    StatusId = RequestStatus.InWork.Id
                });
                
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            ViewData["PlannerId"] = new SelectList(_context.Planners, "Id", "Discriminator", contract.PlannerId);
            ViewData["RequestId"] = new SelectList(_context.Requests, "Id", "Description", contract.RequestId);
            return View(contract);
        }

        // GET: Contract/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            ViewData["PlannerId"] = new SelectList(_context.Planners, "Id", "Discriminator", contract.PlannerId);
            ViewData["RequestId"] = new SelectList(_context.Requests, "Id", "Description", contract.RequestId);
            return View(contract);
        }

        // POST: Contract/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Title,Description,DateCreated,DateStart,DateDeadLine,DateEnd,RequestId,PlannerId")]
            Contract contract)
        {
            if (id != contract.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.Id))
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

            ViewData["PlannerId"] = new SelectList(_context.Planners, "Id", "Discriminator", contract.PlannerId);
            ViewData["RequestId"] = new SelectList(_context.Requests, "Id", "Description", contract.RequestId);
            return View(contract);
        }

        // GET: Contract/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Planner)
                .Include(c => c.Request)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.Id == id);
        }
    }
}