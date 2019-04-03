using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SensenHosp.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public BlogPostsController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;

        }

        // GET: BlogPosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BlogPosts.Include(b => b.BlogCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BlogPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts
                .Include(b => b.BlogCategory)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Create
        public IActionResult Create()
        {
            ViewData["BlogCategoryID"] = new SelectList(_context.BlogCategories, "ID", "Name");
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,DatePublished,Body,BlogCategoryID")] BlogPost blogPost, IFormFile file)
        {
            var webRoot = _env.WebRootPath;
            blogPost.HasImg = 0;
            if (file != null)
            {
                if (file.Length > 0)
                {
                    string[] extensions = { "jpeg", "jpg", "png", "gif" };
                    var extension = Path.GetExtension(file.FileName).Substring(1).ToLower();

                    if (extensions.Contains(extension))
                    {
                        string fn = file.FileName + "." + extension;

                        string path = Path.Combine(webRoot, "Uploads/Blog/FeatureImages");
                        path = Path.Combine(path, fn);

                        //save the file
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            Debug.WriteLine("hey");
                        }
                        //let the model know that there is a picture with an extension
                        blogPost.HasImg = 1;
                        blogPost.ImgName = fn.ToString();

                    }
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogCategoryID"] = new SelectList(_context.BlogCategories, "ID", "Name", blogPost.BlogCategoryID);
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts.SingleOrDefaultAsync(m => m.ID == id);
            if (blogPost == null)
            {
                return NotFound();
            }
            ViewData["BlogCategoryID"] = new SelectList(_context.BlogCategories, "ID", "Name", blogPost.BlogCategoryID);
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,DatePublished,Body,BlogCategoryID")] BlogPost blogPost, IFormFile file)
        {
            var webRoot = _env.WebRootPath;
            
            if (file != null)
            {
                if (file.Length > 0)
                {
                    string[] extensions = { "jpeg", "jpg", "png", "gif" };
                    var extension = Path.GetExtension(file.FileName).Substring(1).ToLower();

                    if (extensions.Contains(extension))
                    {
                        string fn = file.FileName + "." + extension;

                        string path = Path.Combine(webRoot, "Uploads/Blog/FeatureImages");
                        path = Path.Combine(path, fn);

                        //save the file
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            Debug.WriteLine("hey");
                        }
                        //let the model know that there is a picture with an extension
                        blogPost.HasImg = 1;
                        blogPost.ImgName = fn.ToString();

                    }
                }
            }

            if (id != blogPost.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.ID))
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
            ViewData["BlogCategoryID"] = new SelectList(_context.BlogCategories, "ID", "Description", blogPost.BlogCategoryID);
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts
                .Include(b => b.BlogCategory)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _context.BlogPosts.SingleOrDefaultAsync(m => m.ID == id);
            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPosts.Any(e => e.ID == id);
        }
    }
}
