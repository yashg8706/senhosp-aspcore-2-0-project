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
    public class CareersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CareersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {

            return View(_context.Careers.ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career = await _context.Careers
                .SingleOrDefaultAsync(m => m.id == id);
            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        public IActionResult Create()
        {
            ViewData["BlogCategoryID"] = new SelectList(_context.BlogCategories, "ID", "Name");
          //  ViewData["BlogTagID"] = new SelectList(_context.BlogTags, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,title,description,department,type,category,deadline")] Career career)
        {
            if (ModelState.IsValid)
            {
                _context.Add(career);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
             return View(career);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career= await _context.Careers.SingleOrDefaultAsync(m => m.id == id);
            if (career == null)
            {
                return NotFound();
            }
            return View(career);
        }

        // POST: Careers/Edit/1
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,title,description,department,type,category,deadline")] Career career)
        {
            if (id != career.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(career);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CareerExists(career.id))
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
            return View(career);
        }

        private bool CareerExists(int id)
        {
            return _context.Careers.Any(e => e.id == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var career= await _context.Careers
                .SingleOrDefaultAsync(m => m.id == id);
            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        // POST: Careers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var career = await _context.Careers.SingleOrDefaultAsync(m => m.id == id);
            _context.Careers.Remove(career);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}