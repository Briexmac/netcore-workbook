using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaseProject.Data;
using BaseProject.Models;
using BaseProject.Data.Models;

namespace BaseProject.Controllers
{
    [Route("IssueTasks")]
    public class IssueTasksController : Controller
    {
        private readonly IssueTrackerContext _context;

        public IssueTasksController(IssueTrackerContext context)
        {
            _context = context;
        }

        // GET: IssueTasks
        [Route("{issueId:int}")]
        public async Task<IActionResult> IndexByIssue(int issueId)
        {
            var issueTrackerContext = _context.IssueTasks.Where(i => i.IssueId == issueId).Include(i => i.Assignee).Include(i => i.Issue);
            return View(await issueTrackerContext.ToListAsync());
        }

        // GET: IssueTasks/Details/5
        [Route("Details/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueTask = await _context.IssueTasks
                .Include(i => i.Assignee)
                .Include(i => i.Issue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueTask == null)
            {
                return NotFound();
            }

            return View(issueTask);
        }

        // GET: IssueTasks/Create
        [Route("{issueId:int}/Create", Name = "CreateByIssueId")]
        [Route("Create")]
        public IActionResult Create(int? issueId)
        {
            ViewData["AssigneeId"] = new UserSelectList(_context.Users);
            ViewData["IssueId"] = issueId.HasValue ? new IssueSelectList(_context.Issues, issueId.Value) : new IssueSelectList(_context.Issues);
            ViewData["StatusId"] = new StatusSelectList();
            var model = new IssueTaskViewModel {
                CanEditIssue = !issueId.HasValue
            };
            return View(model);
        }

        // POST: IssueTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Status,AssigneeId,IssueId")] IssueTask issueTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueTask);
                await _context.SaveChangesAsync();
                return RedirectToParentIssue(issueTask.IssueId);
            }
            ViewData["AssigneeId"] = issueTask.AssigneeId.HasValue ? new UserSelectList(_context.Users, issueTask.AssigneeId.Value) : new UserSelectList(_context.Users);
            ViewData["IssueId"] = new IssueSelectList(_context.Issues, issueTask.IssueId);
            ViewData["StatusId"] = new StatusSelectList(issueTask.Status);
            return View(issueTask);
        }

        // GET: IssueTasks/Edit/5
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueTask = await _context.IssueTasks.FindAsync(id);
            if (issueTask == null)
            {
                return NotFound();
            }
            ViewData["AssigneeId"] = issueTask.AssigneeId.HasValue ? new UserSelectList(_context.Users, issueTask.AssigneeId.Value) : new UserSelectList(_context.Users);
            ViewData["IssueId"] = new IssueSelectList(_context.Issues, issueTask.IssueId);
            ViewData["StatusId"] = new StatusSelectList();
            return View(issueTask);
        }

        // POST: IssueTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status,AssigneeId,IssueId")] IssueTask issueTask)
        {
            if (id != issueTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueTaskExists(issueTask.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToParentIssue(issueTask.IssueId);
            }
            ViewData["AssigneeId"] = issueTask.AssigneeId.HasValue ? new UserSelectList(_context.Users, issueTask.AssigneeId.Value) : new UserSelectList(_context.Users);
            ViewData["IssueId"] = new IssueSelectList(_context.Issues, issueTask.IssueId);
            ViewData["StatusId"] = new StatusSelectList(issueTask.Status);
            return View(issueTask);
        }

        // GET: IssueTasks/Delete/5
        [HttpGet]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueTask = await _context.IssueTasks
                .Include(i => i.Assignee)
                .Include(i => i.Issue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueTask == null)
            {
                return NotFound();
            }

            return View(issueTask);
        }

        // POST: IssueTasks/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [Route("Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issueTask = await _context.IssueTasks.FindAsync(id);
            _context.IssueTasks.Remove(issueTask);
            await _context.SaveChangesAsync();
            return RedirectToParentIssue(issueTask.IssueId);
        }

        #region Private

        private bool IssueTaskExists(int id)
        {
            return _context.IssueTasks.Any(e => e.Id == id);
        }

        private IActionResult RedirectToParentIssue(int issueId)
        {
            return RedirectToAction("Details", "Issues", new { id = issueId });
        }

        #endregion
    }
}
