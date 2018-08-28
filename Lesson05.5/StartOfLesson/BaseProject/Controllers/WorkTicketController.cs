using System.Linq;
using System.Threading.Tasks;
using BaseProject.BillingLogic;
using BaseProject.Data;
using BaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Controllers
{
    public class WorkTicketController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly BillingAdaptor _billingAdaptor;
        private readonly BillingFacade _billingFacade;
        private readonly BillingFactory _billingFactory;

        public WorkTicketController(
            ApplicationContext context,
            BillingAdaptor billingAdaptor,
            BillingFacade billingFacade,
            BillingFactory billingFactory
            )
        {
            _context = context;
            _billingAdaptor = billingAdaptor;
            _billingFacade = billingFacade;
            _billingFactory = billingFactory;
        }

        // GET: WorkTicket
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkTickets.ToListAsync());
        }

        // GET: WorkTicket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTicket = await _context.WorkTickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTicket == null)
            {
                return NotFound();
            }

            return View(workTicket);
        }

        // GET: WorkTicket/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkTicket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Start,End,IsExpat")] WorkTicket workTicket)
        {
            if (ModelState.IsValid)
            {
                CalcuateCost(workTicket);
                _context.Add(workTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workTicket);
        }

        private void CalcuateCost(WorkTicket workTicket)
        {
            //TODO: Today's lesson



        }

        // GET: WorkTicket/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTicket = await _context.WorkTickets.FindAsync(id);
            if (workTicket == null)
            {
                return NotFound();
            }
            return View(workTicket);
        }

        // POST: WorkTicket/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Start,End,IsExpat")] WorkTicket workTicket)
        {
            if (id != workTicket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CalcuateCost(workTicket);
                    _context.Update(workTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkTicketExists(workTicket.Id))
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
            return View(workTicket);
        }

        // GET: WorkTicket/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workTicket = await _context.WorkTickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTicket == null)
            {
                return NotFound();
            }

            return View(workTicket);
        }

        // POST: WorkTicket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workTicket = await _context.WorkTickets.FindAsync(id);
            _context.WorkTickets.Remove(workTicket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkTicketExists(int id)
        {
            return _context.WorkTickets.Any(e => e.Id == id);
        }
    }
}
