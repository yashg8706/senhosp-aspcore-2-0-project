using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SensenHosp.Data;
using SensenHosp.Models;

namespace SensenHosp.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public AlbumsController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Albums
        public async Task<IActionResult> Index(int pagenum = 0)
        {
            var _album = await _context.Albums
                .Include(a => a.Media)
                .ToListAsync();

            int albumcount = _album.Count;

            if (albumcount == 0)
            {
                return RedirectToAction("Create");
            }

            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)albumcount / perpage) - 1;

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

            var album = await _context.Albums
                .Include(a => a.Media)
                .Skip(start)
                .Take(perpage)
                .ToListAsync();

            return View(album);
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id, int pagenum = 0)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Media)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (album == null)
            {
                return NotFound();
            }

            int mediacount = album.Media.Count;
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)mediacount / perpage) - 1;
            ViewData["maxpage"] = (int)maxpage;
            ViewData["pagenum"] = (int)pagenum;
            ViewData["perpage"] = (int)perpage;

            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum + 1).ToString() + " of " + (maxpage + 1).ToString();
            }
            else
            {
                ViewData["pagesummary"] = "1 of 1";
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description")] Album album)
        {
            if (ModelState.IsValid)
            {
                _context.Add(album);
                await _context.SaveChangesAsync();

                var webRoot = _env.WebRootPath;
                var albumRoot = System.IO.Path.Combine(webRoot, "Uploads\\Media\\Albums");
                var folder = System.IO.Path.Combine(albumRoot, album.Title);
                if (!Directory.Exists(folder))
                {
                    DirectoryInfo di = Directory.CreateDirectory(folder);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.SingleOrDefaultAsync(m => m.ID == id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description")] Album album)
        {
            if (id != album.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.ID))
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
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .SingleOrDefaultAsync(m => m.ID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.SingleOrDefaultAsync(m => m.ID == id);
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(e => e.ID == id);
        }
    }
}
