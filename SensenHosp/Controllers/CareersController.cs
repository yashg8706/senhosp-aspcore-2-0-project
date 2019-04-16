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

        public async Task<ActionResult> Index(int pagenum)
        {
            ViewData["UserRole"] = "Admin";
            /*Careers PAGINATION ALGORITHM*/
            var _careers = await _context.Careers.ToListAsync();
            int careercount = _careers.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)careercount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;
            int start = perpage * pagenum;
            ViewData["pagenum"] = (int)pagenum;
            ViewData["PaginationSummary"] = "";
            if (maxpage > 0)
            {
                ViewData["PaginationSummary"] =
                    (pagenum + 1).ToString() + " of " +
                    (maxpage + 1).ToString();
            }
            //DATA NEEDED: All Blogs in DB
            //However, we also have to include the info for the author on each blog
            //[List<Blog> blogs]=> variable named blogs which is a list of Blog
            //[.Include(b=>b.Author)]=> Get the blog's associated author
            //[.ToListAsync()]=>Return a list of information asynchronously

            //[.Skip(int)]=>ignore these many records
            //[.Take(int)]=>fetch only this many more
            List<Career> careers = await _context.Careers.Skip(start).Take(perpage).ToListAsync();
            /*END OF BLLOG PAGINATION ALGORITHM*/
            return View(careers);



        }

        public async Task<IActionResult> Details(int? id)
        {
            Career located_career = await _context.Careers.Include(a => a.Applicants).SingleOrDefaultAsync(a => a.id == id);
            if (located_career == null)
            {
                return NotFound();
            }
            return View(located_career);
            /*if (id == null)
            {
                return NotFound();
            }

            var career = await _context.Careers
                .SingleOrDefaultAsync(m => m.id == id);
            if (career == null)
            {
                return NotFound();
            }

            return View(career);*/
        }

        public IActionResult Create()
        {
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