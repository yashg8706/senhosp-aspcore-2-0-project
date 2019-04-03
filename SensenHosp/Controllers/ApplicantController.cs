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
using System.IO;

namespace SensenHosp.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _env;

        public ApplicantController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Applicants.Include(m => m.Career);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .Include(m => m.Career)
                .SingleOrDefaultAsync(m => m.id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
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
        public async Task<IActionResult> Create([Bind("fname,lname,email,contact,career_id")] Applicant applicant, IFormFile file)
        {
            var webRoot = _env.WebRootPath;

            var careerName = _context.Careers.Find(applicant.career_id).title;

            if (file != null)
            {
                if (file.Length > 0)
                {
                    string[] extensions = { "jpeg", "jpg", "png", "gif", "avi", "mp4" };
                    var extension = Path.GetExtension(file.FileName).Substring(1);

                    if (extensions.Contains(extension))
                    {
                        string fn = applicant.email + "." + extension;

                        string path = Path.Combine(webRoot, "Uploads/Resume", careerName);
                        path = Path.Combine(path, fn);

                        //save the file
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        //let the model know that there is a resume with an extension
                        applicant.resume = extension.ToString();

                    }
                }
            }
            Debug.WriteLine("hey");
            if (ModelState.IsValid)
            {
                _context.Add(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AlbumID"] = new SelectList(_context.Albums, "ID", "Title", media.AlbumID);
            return View(applicant);
        }

    }
}