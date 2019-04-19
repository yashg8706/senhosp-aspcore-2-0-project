using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SensenHosp.Models;
using SensenHosp.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SensenHosp.Controllers
{
    public class ReviewOnDoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewOnDoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: ReviewOnDoctor/Create
        public async Task<IActionResult> Create()
        {
            ViewData["physicianId"] = new SelectList(_context.physician, "physicianId", "physicianName");
            return View();
        }

        //POST: ReviewOnDoctor/Create
        [HttpPost]
        public async Task<IActionResult> Create(string physicianId, string Message)
        {
            string query = "insert into ReviewOnDoctor(physicianId,Message)" +
                "values(@doctorname,@message)";
            SqlParameter[] myparams = new SqlParameter[2];

            myparams[0] = new SqlParameter("@doctorname", physicianId);
            myparams[1] = new SqlParameter("@message", Message);

            _context.Database.ExecuteSqlCommand(query, myparams);
            return RedirectToAction(nameof(List));
        }
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ReviewId", "DoctorName", "physicianId", "Message", "Reply")] ReviewOnDoctor review)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(review);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(List));
        //    }
        //    return View(review);
        //}
        //GET: Reviews
        public async Task<IActionResult> List(int pagenum)
        {
            var _review = await _context.ReviewOnDoctor.ToListAsync();
            int count = _review.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)count / perpage) - 1;
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
            List<ReviewOnDoctor> review = await _context.ReviewOnDoctor.Include(a=>a.Physician).Skip(start).Take(perpage).ToListAsync();
            //await _context.ReviewOnDoctor.Skip(start).Take(perpage).ToListAsync();
            return View(review);
            //return View(await _context.ReviewOnDoctor.ToListAsync());
        }

        //GET: ReviewOnDoctor/Edit/5
        public async Task<IActionResult> Edit(int?id)
        {
            if (id == null)
            {
                return new StatusCodeResult(400);
            }
            ReviewOnDoctor review = _context.ReviewOnDoctor.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["physicianId"] = new SelectList(_context.physician, "physicianId", "physicianName");
            return View(review);
        }
        //POST: ReviewOnDoctor/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, string physicianId, string Message, string Reply)
        {
            if (id == null || (_context.ReviewOnDoctor.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update ReviewOnDoctor set " +
                "physicianId = @doctorname," +
                "Message = @message," +
                "Reply = @reply " +
                "where ReviewId = @id";

            SqlParameter[] myparams = new SqlParameter[4];
            myparams[0] = new SqlParameter("@id",id);
            myparams[1] = new SqlParameter("@doctorname", physicianId);
            myparams[2] = new SqlParameter("@message", Message);
            myparams[3] = new SqlParameter("@reply", Reply);

            _context.Database.ExecuteSqlCommand(query, myparams);
            return RedirectToAction("List");
            //return View(nameof(List));
        }
        //public async Task<IActionResult> Edit(int id,[Bind("ReviewId", "DoctorName", "Message", "Reply")] ReviewOnDoctor review)
        //{
        //    if(id != review.ReviewId)
        //    {
        //        return NotFound();
        //    }
        //    if(ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(review);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ReviewOnDoctorExists(review.ReviewId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(List));
        //    }
        //    return View(review);
        //}

        //GET: ReviewOnDoctor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(400);
            }
            ReviewOnDoctor review = _context.ReviewOnDoctor.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            //ViewData["physicianId"] = await _context.ReviewOnDoctor.Include(a => a.Physician).Skip(start).Take(perpage).ToListAsync();
            ViewData["physicianId"] = new SelectList(_context.physician, "physicianId", "physicianName");
            return View(review);
        }

        //GET: ReviewOnDoctor/Delete/1
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.ReviewOnDoctor.SingleOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        //POST: ReviewOnDoctor/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.ReviewOnDoctor.SingleOrDefaultAsync(m => m.ReviewId == id);
            _context.ReviewOnDoctor.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));

        }

        private bool ReviewOnDoctorExists(int id)
        {
            return _context.ReviewOnDoctor.Any(e => e.ReviewId == id);
        }
    }
}