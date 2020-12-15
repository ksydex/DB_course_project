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
        public async Task<IActionResult> Index([FromQuery] int statusId = 0)
        {
            var applicationContext = statusId == 0
                ? _db.Requests.OrderByDescending(x => x.Id)
                : _db.Requests
                    .Where(x => x.StatusHistory
                        .OrderByDescending(y => y.Id)
                        .Take(1).FirstOrDefault().StatusId == statusId);

            ViewData["statusId"] = statusId;

            return View(await applicationContext.ToListAsync());
        }
        
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

        public async Task<IActionResult> SetStatus(int id, [FromQuery] int statusId)
        {
            var request = _db.Requests.FirstOrDefault(x => x.Id == id);
            if (request != null && await _db.RequestStatuses.AnyAsync(x => x.Id == statusId))
            {
                await _db.RequestStatusHistories.AddAsync(new RequestStatusHistory
                {
                    EntityId = request.Id,
                    StatusId = statusId
                });
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Request", new { Area = "Planner", statusId = 0 });
        }
    }
}