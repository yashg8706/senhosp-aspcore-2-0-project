using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SensenHosp.Data;
using SensenHosp.Models;
using System.Diagnostics;

namespace SensenHosp.Controllers
{
    public class MediaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public MediaController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Media
        public async Task<IActionResult> Index(int pagenum = 0)
        {
            var applicationDbContext = _context.Media.Include(m => m.Album);

            var _media = await _context.Media
                .Include(a => a.Album)
                .ToListAsync();

            int mediacount = _media.Count;

            if (mediacount == 0)
            {
                return RedirectToAction("Create");
            }

            int perpage = 6;
            int maxpage = (int)Math.Ceiling((decimal)mediacount / perpage) - 1;

            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;

            int start = pagenum * perpage;

            ViewData["maxpage"] = (int)maxpage;
            ViewData["pagenum"] = (int)pagenum;

            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum + 1).ToString() + " of " + (maxpage + 1).ToString();
            }
            else
            {
                ViewData["pagesummary"] = "1 of 1";
            }

            var media = await _context.Media
                .Include(a => a.Album)
                .Skip(start)
                .Take(perpage)
                .ToListAsync();

            return View(media);
        }

        // GET: Media/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Media
                .Include(m => m.Album)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // GET: Media/Create
        public IActionResult Create()
        {
            ViewData["AlbumID"] = new SelectList(_context.Albums, "ID", "Title");
            return View();
        }

        // POST: Media/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,AlbumID")] Media media, IFormFile file)
        {
            var webRoot = _env.WebRootPath;

            var albumName = _context.Albums.Find(media.AlbumID).Title;

            if (file != null)
            {
                if (file.Length > 0)
                {
                    string[] extensions = { "jpeg", "jpg", "png", "gif", "avi", "mp4" };
                    var extension = Path.GetExtension(file.FileName).Substring(1).ToLower();

                    if (extensions.Contains(extension))
                    {
                        string fn = media.Name+ "." + extension;
                        
                        string path = Path.Combine(webRoot, "Uploads/Media/Albums", albumName);
                        path = Path.Combine(path, fn);

                        //save the file
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        //let the model know that there is a picture with an extension
                        media.Extension = extension.ToString();

                    }
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(media);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumID"] = new SelectList(_context.Albums, "ID", "Title", media.AlbumID);
            return View(media);
        }

        // GET: Media/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Media.SingleOrDefaultAsync(m => m.ID == id);
            if (media == null)
            {
                return NotFound();
            }
            ViewData["AlbumID"] = new SelectList(_context.Albums, "ID", "Title", media.AlbumID);
            return View(media);
        }

        // POST: Media/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Extension,AlbumID")] Media media)
        {
            if (id != media.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(media);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaExists(media.ID))
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
            ViewData["AlbumID"] = new SelectList(_context.Albums, "ID", "Title", media.AlbumID);
            return View(media);
        }

        // GET: Media/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var media = await _context.Media
                .Include(m => m.Album)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (media == null)
            {
                return NotFound();
            }

            return View(media);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var media = await _context.Media.SingleOrDefaultAsync(m => m.ID == id);
            _context.Media.Remove(media);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaExists(int id)
        {
            return _context.Media.Any(e => e.ID == id);
        }
    }
}
