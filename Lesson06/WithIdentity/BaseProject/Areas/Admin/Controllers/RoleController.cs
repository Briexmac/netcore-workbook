using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseProject.Areas.Admin.Infrustructure;
using BaseProject.Areas.Admin.Models;
using BaseProject.Areas.Identity;
using BaseProject.Areas.Identity.Data;
using BaseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/Role")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IdentityContext _context;

        public RoleController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Admin/Role
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var assignedUsers = await GetRolesForListView();
            return View(assignedUsers);
        }

        // GET: Admin/Role/Details/5
        [Route("{id:guid}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await GetBaseViewViewModel(id);
            if (viewModel.Role == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // GET: Admin/Role/Create
        [Route("Create")]
        public async Task<IActionResult> Create()
        {
            var viewModel = await GetBaseEditViewModel(null);
            return View(viewModel);
        }

        // POST: Admin/Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("Id,Role,AssignedUsers")] EditRoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Role.NormalizedName = viewModel.Role.Name.ToUpper();
                _context.Roles.Add(viewModel.Role);
                await UpdateUsersAssignedToRole(viewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            viewModel.AvailableUsers = await GetLatestAvailableUsers(viewModel.AssignedUsers);
            return View(viewModel);
        }

        // GET: Admin/Role/Edit/5
        [Route("Edit/{id:guid}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await GetBaseEditViewModel(id);
            if (viewModel.Role == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Admin/Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:guid}")]
        [PreventAdminRoleChange]
        [PreventNoAdmin]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Role,AssignedUsers")] EditRoleViewModel viewModel)
        {
            if (id != viewModel.Role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    viewModel.Role.NormalizedName = viewModel.Role.Name.ToUpper();
                    _context.Roles.Update(viewModel.Role);
                    await UpdateUsersAssignedToRole(viewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(viewModel.Role.Id))
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
            viewModel.AvailableUsers = await GetLatestAvailableUsers(viewModel.AssignedUsers);
            return View(viewModel);
        }

        // GET: Admin/Role/Delete/5
        [Route("Delete/{id:guid}")]
        [PreventAdminRoleChange]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = await GetBaseViewViewModel(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Admin/Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var role = await _context.Roles.FindAsync(id);
            if (role.IsAdmin())
            {
                return BadRequest();
            }
            var assignedUsers = await _context.UserRoles.Where(x => x.RoleId == id).ToListAsync();
            _context.UserRoles.RemoveRange(assignedUsers);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(string id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }

        private async Task UpdateUsersAssignedToRole(EditRoleViewModel viewModel)
        {
            var existingUsers = await _context.UserRoles.Where(x => x.RoleId == viewModel.Role.Id).ToListAsync();
            _context.UserRoles.RemoveRange(existingUsers);
            if (viewModel.AssignedUsers != null)
                _context.UserRoles.AddRange(viewModel.AssignedUsers.Select(x => new IdentityUserRole<string> { RoleId = viewModel.Role.Id, UserId = x }));
        }

        private async Task<MultiSelectList> GetLatestAvailableUsers(List<string> assignedUsers)
        {
            return new MultiSelectList(await _context.Users.AsNoTracking().ToListAsync(), nameof(IdentityUser.Id), nameof(IdentityUser.UserName), assignedUsers ?? new List<string>());
        }

        private async Task<EditRoleViewModel> GetBaseEditViewModel(string roleId)
        {
            IdentityRole role = null;
            List<string> assignedUserIds = new List<string>();
            if (roleId != null)
            {
                role = await _context.Roles.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == roleId);
                assignedUserIds = await _context.UserRoles.AsNoTracking()
                    .Where(x => x.RoleId == roleId)
                    .Select(x => x.UserId)
                    .ToListAsync();
            }

            var availableUsers = await _context.Users.AsNoTracking().ToListAsync();
            var availableUsersSelectList = new MultiSelectList(availableUsers, nameof(IdentityUser.Id), nameof(IdentityUser.UserName), assignedUserIds);
            return new EditRoleViewModel(role)
            {
                AssignedUsers = assignedUserIds,
                AvailableUsers = availableUsersSelectList
            };
        }

        private async Task<ViewRoleViewModel> GetBaseViewViewModel(string roleId)
        {
            IdentityRole role = null;
            List<User> assignedUsers = new List<User>();
            if (roleId != null)
            {
                role = await _context.Roles.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == roleId);
                assignedUsers = await _context.UserRoles.AsNoTracking()
                    .Where(x => x.RoleId == roleId)
                    .Join(_context.Users, x => x.UserId, x => x.Id, (roleUser, user) => user)
                    .ToListAsync();
            }

            return new ViewRoleViewModel(role)
            {
                AssignedUsers = assignedUsers,
            };
        }

        private async Task<List<ViewRoleViewModel>> GetRolesForListView()
        {
            var usersWithRole = _context.UserRoles.AsNoTracking()
                            .Join(_context.Users.AsNoTracking(), x => x.UserId, x => x.Id, (roleUser, user) => new { User = user, roleUser.RoleId });
            var assignedUsers = await _context.Roles.AsNoTracking()
                .GroupJoin(usersWithRole, x => x.Id, x => x.RoleId, (role, group) => new ViewRoleViewModel(role) { AssignedUsers = group.Select(x => x.User).ToList() })
                .ToListAsync();
            return assignedUsers;
        }
    }
}
