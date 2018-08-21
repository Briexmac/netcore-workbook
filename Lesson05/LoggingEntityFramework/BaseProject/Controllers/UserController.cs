using System.Threading;
using System.Threading.Tasks;
using BaseProject.Data;
using BaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly ApplicationContext _context;

        public UserController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        [HttpGet]
        [Route("Create")]
        // GET: Users
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        // GET: Users
        public async Task<IActionResult> Create(User user, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(user);
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}
