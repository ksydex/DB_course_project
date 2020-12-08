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

namespace ContractAndProjectManager.Areas.Customer.Controllers
{
    [Authorize(Roles = Role.Keys.Customer)]
    [Area("Customer")]
    public class RequestController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly UserService _userService;

        public RequestController(ApplicationContext db, UserService userService)
        {
            _db = db;
            _userService = userService;
        }

        // GET: Request
        public async Task<IActionResult> Index()
        {
            var applicationContext = _db.Requests.Include(r => r.Customer);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Request/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _db.Requests
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Request/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_db.Customers, "Id", "Discriminator");
            return View();
        }

        // POST: Request/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,DateDeadLine")] Request request)
        {
            if (ModelState.IsValid)
            {
                request.CustomerId = _userService.UserId;
                _db.Add(request);
                await _db.SaveChangesAsync();

                await _db.RequestStatusHistories.AddAsync(new RequestStatusHistory
                {
                    EntityId = request.Id,
                    StatusId = RequestStatus.Pending.Id
                });
                
                await _db.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: Request/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _db.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }

        // POST: Request/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,DateDeadLine,CustomerId")] Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(request);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.Id))
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
            return View(request);
        }

        // GET: Request/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _db.Requests
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _db.Requests.FindAsync(id);
            _db.Requests.Remove(request);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
            return _db.Requests.Any(e => e.Id == id);
        }
    }
}
