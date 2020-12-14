using System.Linq;
using System.Threading.Tasks;
using ContractAndProjectManager.Data;
using ContractAndProjectManager.Entities;
using ContractAndProjectManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractAndProjectManager.Areas.Customer.Controllers
{
    [Authorize(Roles = Role.Keys.Customer)]
    [Area("Customer")]
    public class ContractController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserService _userService;

        public ContractController(ApplicationContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var myRequestsIds =
                (await _context.Requests.Where(x => x.CustomerId == _userService.UserId).ToListAsync()).Select(
                    x => x.Id);
            var applicationContext = _context.Contracts.Where(x => myRequestsIds.Contains(x.RequestId));
            return View(await applicationContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myRequestsIds =
                (await _context.Requests.Where(x => x.CustomerId == _userService.UserId).ToListAsync()).Select(
                    x => x.Id);

            var contract =
                await _context.Contracts.FirstOrDefaultAsync(m => m.Id == id && myRequestsIds.Contains(m.RequestId));
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }
    }
}