using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SensenHosp.Data;
using SensenHosp.Models;

namespace SensenHosp.Controllers
{
    public class BlogCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private async Task<ApplicationUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(HttpContext.User);

        public BlogCategoriesController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }

        public async Task<dynamic> GetUserId()
        {
            ApplicationUser user = new ApplicationUser();
            user = await GetCurrentUserAsync();
            if (user != null)
            {
                return (int)user.UserID;
            }
            else
            {
                return null;
            }

        }

        // GET: BlogCategories
        public async Task<IActionResult> Index(int pagenum)
        {
            ViewData["user"] = await GetUserId();

            var _blogcategory = await _context.BlogCategories
                .Include(a => a.BlogPosts)
                .ToListAsync();

            int categorycount = _blogcategory.Count;

            if (categorycount == 0)
            {
                return RedirectToAction("Create");
            }

            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)categorycount / perpage) - 1;

            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;

            int start = pagenum * perpage;

            ViewData["maxpage"] = (int)maxpage;
            ViewData["pagenum"] = (int)pagenum;
            ViewData["pagesummary"] = "";

            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum + 1).ToString() + " of " + (maxpage + 1).ToString();
            }
            else
            {
                ViewData["pagesummary"] = "1 of 1";
            }

            var blogcategory = await _context.BlogCategories
                .Include(a => a.BlogPosts)
                .Skip(start)
                .Take(perpage)
                .ToListAsync();

            return View(blogcategory);
        }

        // GET: BlogCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["user"] = await GetUserId();

            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategories
                .SingleOrDefaultAsync(m => m.ID == id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        // GET: BlogCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description")] BlogCategory blogCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogCategory);
        }

        // GET: BlogCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategories.SingleOrDefaultAsync(m => m.ID == id);
            if (blogCategory == null)
            {
                return NotFound();
            }
            return View(blogCategory);
        }

        // POST: BlogCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description")] BlogCategory blogCategory)
        {
            if (id != blogCategory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogCategoryExists(blogCategory.ID))
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
            return View(blogCategory);
        }

        // GET: BlogCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategories
                .SingleOrDefaultAsync(m => m.ID == id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        // POST: BlogCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogCategory = await _context.BlogCategories.SingleOrDefaultAsync(m => m.ID == id);
            _context.BlogCategories.Remove(blogCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogCategoryExists(int id)
        {
            return _context.BlogCategories.Any(e => e.ID == id);
        }
    }
}
