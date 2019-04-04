using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SensenHosp.Data;
using SensenHosp.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace SensenHosp.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: Roles/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        //POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleID,RoleName,RoleDescription")] SensenHosp.Models.UserRole user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(user);
        }
        //GET: Roles
        public async Task<IActionResult> List()
        {
            return View(await _context.UserRole.ToListAsync());
        }

        //GET: UserRole/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userrole = await _context.UserRole.SingleOrDefaultAsync(m => m.RoleID == id);
            if (userrole == null)
            {
                return NotFound();
            }
            return View(userrole);
        }

        //GET: UserRole/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userrole = await _context.UserRole.SingleOrDefaultAsync(m => m.RoleID == id);
            if (userrole == null)
            {
                return NotFound();
            }
            return View(userrole);
        }

        //POST: UserRole/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("RoleID,RoleName,RoleDescription")] UserRole userrole)
        {
            if (id != userrole.RoleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userrole);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!UserRoleExists(userrole.RoleID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(userrole);
        }

        //GET: UserRole/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userrole = await _context.UserRole.SingleOrDefaultAsync(m => m.RoleID == id);
            if (userrole == null)
            {
                return NotFound();
            }
            return View(userrole);
        }

        //POST: UserRole/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userrole = await _context.UserRole.SingleOrDefaultAsync(m=>m.RoleID == id);
            _context.UserRole.Remove(userrole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
                
        }

        private bool UserRoleExists(int id)
        {
            return _context.UserRole.Any(e => e.RoleID == id);
        }
    }
}