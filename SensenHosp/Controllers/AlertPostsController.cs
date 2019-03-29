using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SensenHosp.Data;
using SensenHosp.Models;

namespace SensenHosp.Controllers
{
    public class AlertPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlertPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AlertPosts
        public async Task<IActionResult> Index()
        {
            return View(await _context.AlertPosts.ToListAsync());
        }

        // GET: AlertPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alertPosts = await _context.AlertPosts
                .SingleOrDefaultAsync(m => m.ID == id);
            if (alertPosts == null)
            {
                return NotFound();
            }

            return View(alertPosts);
        }

        // GET: AlertPosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AlertPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AlertTitle,Description,AlertStatus,DateCreated,DateEffectivity")] AlertPosts alertPosts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alertPosts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alertPosts);
        }

        // GET: AlertPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alertPosts = await _context.AlertPosts.SingleOrDefaultAsync(m => m.ID == id);
            if (alertPosts == null)
            {
                return NotFound();
            }
            return View(alertPosts);
        }

        // POST: AlertPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,AlertTitle,Description,AlertStatus,DateCreated,DateEffectivity")] AlertPosts alertPosts)
        {
            if (id != alertPosts.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alertPosts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlertPostsExists(alertPosts.ID))
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
            return View(alertPosts);
        }

        // GET: AlertPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alertPosts = await _context.AlertPosts
                .SingleOrDefaultAsync(m => m.ID == id);
            if (alertPosts == null)
            {
                return NotFound();
            }

            return View(alertPosts);
        }

        // POST: AlertPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alertPosts = await _context.AlertPosts.SingleOrDefaultAsync(m => m.ID == id);
            _context.AlertPosts.Remove(alertPosts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlertPostsExists(int id)
        {
            return _context.AlertPosts.Any(e => e.ID == id);
        }
    }
}
