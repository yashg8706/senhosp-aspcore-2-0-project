using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SensenHosp.Data;
using SensenHosp.Models;
using Microsoft.AspNetCore.Identity;

namespace SensenHosp.Controllers
{
    public class AlertPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        //THIS IS SUPPOSED TO BE FOR ADMIN FUNCTIONALITY BUT IT THORWS AN ERROR AND WE CAN'T REGISTER OR LOGIN
        //private readonly UserManager<ApplicationUser> _userManager;
        //private async Task<ApplicationUser> GetCurrentUserAsync() => await _userManager.GetUserAsync(HttpContext.User);

        public AlertPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<dynamic> GetUserId()
        //{
        //    ApplicationUser user = new ApplicationUser();
        //    user = await GetCurrentUserAsync();
        //    if (user != null)
        //    {
        //        return (int)user.UserID;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}

        // GET: AlertPosts
        public async Task<IActionResult> Index()
        {

            return View(await _context.AlertPosts.ToListAsync());
        }

        public async Task<IActionResult> Admin(int pagenum)
        {
            //ViewData["user"] = await GetUserId();

            var _alertPost = await _context.AlertPosts.ToListAsync();

            int alertcount = _alertPost.Count();
            int perpage = 5;
            int maxpage = (int)Math.Ceiling((decimal)alertcount / perpage) - 1;

            if (maxpage < 0) maxpage = 0;

            if (pagenum < 0) pagenum = 0;

            if (pagenum > maxpage) pagenum = maxpage;

            int start = perpage * pagenum;

            ViewData["pagenum"] = (int)pagenum;

            ViewData["pagesummary"] = "";

            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum + 1).ToString() + " of " + (maxpage + 1).ToString();
            }
            else
            {
                ViewData["pagesummary"] = "Page 1 of 1";
            }

            //THE LIST WILL BE IN DESCENDING ORDER SO ADMIN USER CAN SEE THE MOST RECENT CREATED ALERTPOST
            List<AlertPosts> alertPosts = await _context.AlertPosts.OrderByDescending(m => m.DateCreated).Skip(start).Take(perpage).ToListAsync();
            return View(alertPosts);
        }
        // GET: AlertPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //ViewData["user"] = await GetUserId();

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
        public async Task<IActionResult> Create([Bind("ID,AlertTitle,Description,AlertStatus,DateCreated,DateEffectivity")] AlertPosts alertPosts, string AlerpostStatus)
        {
            if (ModelState.IsValid)
            {
                alertPosts.DateCreated = DateTime.Now;
                alertPosts.AlertStatus = AlerpostStatus;
                _context.Add(alertPosts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Admin));
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,AlertTitle,Description,AlertStatus,DateEffectivity")] AlertPosts alertPosts, string AlerpostStatus)
        {
            if (id != alertPosts.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    alertPosts.DateCreated = DateTime.Now;
                    alertPosts.AlertStatus = AlerpostStatus;
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
                return RedirectToAction(nameof(Admin));
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
            return RedirectToAction(nameof(Admin));
        }

        private bool AlertPostsExists(int id)
        {
            return _context.AlertPosts.Any(e => e.ID == id);
        }
    }
}
