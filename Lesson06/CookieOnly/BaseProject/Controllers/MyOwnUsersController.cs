using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BaseProject.Data;
using BaseProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Controllers
{
    [Route("MyOwnUsers")]
    public class MyOwnUsersController : Controller
    {
        private readonly ApplicationContext _context;

        public MyOwnUsersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: SignIn
        [Route("SignIn")]
        public async Task<IActionResult> SignIn()
        {
            return View();
        }

        // GET: SignIn
        [Route("Logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        // POST: SignIn
        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn([Bind("UserName,Password")]MyOwnUser user)
        {
            if (!ModelState.IsValid)
            {
                // Blank out the password so we don't send it back
                user.Password = null;
                return View(user);
            }
            if (!await LoginIsValid(user))
            {
                ModelState.AddModelError("", "Invalid UserName/Password");
                return View();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.Now.AddMinutes(10)
                });
            return RedirectToAction("Index", "Issue");
        }

        private async Task<bool> LoginIsValid(MyOwnUser user)
        {
            using (var cancellationTokenSource = new CancellationTokenSource(1000))
            {
                var foundUserTask = _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName && x.Password == user.Password, cancellationTokenSource.Token);
                var loginTookTooLong = false;
                try
                {
                    // crude way of making sure no one knows if the user exists or not.
                    // this also lets the login process take a while so no one can brute force it quickly
                    await Task.WhenAll(foundUserTask, Task.Delay(1000));
                }
                catch (TaskCanceledException) { loginTookTooLong = true; }
                // here we check if login took longer than expected or if the username and password were wrong (aka: didn't return a record)
                return !loginTookTooLong && foundUserTask.Result != null;
            }
        }

        // GET: MyOwnUsers
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: MyOwnUsers/Details/5
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myOwnUser = await _context.Users
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (myOwnUser == null)
            {
                return NotFound();
            }

            return View(myOwnUser);
        }

        // GET: MyOwnUsers/Create
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyOwnUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("UserName,Password,IsAdmin")] MyOwnUser myOwnUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myOwnUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myOwnUser);
        }

        // GET: MyOwnUsers/Edit/5
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myOwnUser = await _context.Users.FindAsync(id);
            if (myOwnUser == null)
            {
                return NotFound();
            }
            return View(myOwnUser);
        }

        // POST: MyOwnUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("UserName,Password,IsAdmin")] MyOwnUser myOwnUser)
        {
            if (id != myOwnUser.UserName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myOwnUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyOwnUserExists(myOwnUser.UserName))
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
            return View(myOwnUser);
        }

        // GET: MyOwnUsers/Delete/5
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myOwnUser = await _context.Users
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (myOwnUser == null)
            {
                return NotFound();
            }

            return View(myOwnUser);
        }

        // POST: MyOwnUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var myOwnUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(myOwnUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyOwnUserExists(string id)
        {
            return _context.Users.Any(e => e.UserName == id);
        }
    }
}
