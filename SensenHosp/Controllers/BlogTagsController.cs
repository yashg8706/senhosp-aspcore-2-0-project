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
    public class BlogTagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogTagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BlogTags
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogTags.ToListAsync());
        }

        // GET: BlogTags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTag = await _context.BlogTags
                .SingleOrDefaultAsync(m => m.ID == id);
            if (blogTag == null)
            {
                return NotFound();
            }

            return View(blogTag);
        }

        // GET: BlogTags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogTags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] BlogTag blogTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogTag);
        }

        // GET: BlogTags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTag = await _context.BlogTags.SingleOrDefaultAsync(m => m.ID == id);
            if (blogTag == null)
            {
                return NotFound();
            }
            return View(blogTag);
        }

        // POST: BlogTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] BlogTag blogTag)
        {
            if (id != blogTag.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogTagExists(blogTag.ID))
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
            return View(blogTag);
        }

        // GET: BlogTags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogTag = await _context.BlogTags
                .SingleOrDefaultAsync(m => m.ID == id);
            if (blogTag == null)
            {
                return NotFound();
            }

            return View(blogTag);
        }

        // POST: BlogTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogTag = await _context.BlogTags.SingleOrDefaultAsync(m => m.ID == id);
            _context.BlogTags.Remove(blogTag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogTagExists(int id)
        {
            return _context.BlogTags.Any(e => e.ID == id);
        }
    }
}
