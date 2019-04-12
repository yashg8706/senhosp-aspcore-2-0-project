using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SensenHosp.Models;
using SensenHosp.Data;
using Microsoft.EntityFrameworkCore;

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
            return View();
        }

        //POST: ReviewOnDoctor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId", "DoctorName", "Message", "Reply")] ReviewOnDoctor review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(review);
        }
        //GET: Reviews
        public async Task<IActionResult> List()
        {
            return View(await _context.ReviewOnDoctor.ToListAsync());
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
            return View(review);
        }
        //POST: ReviewOnDoctor/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("ReviewId", "DoctorName", "Message", "Reply")] ReviewOnDoctor review)
        {
            if(id != review.ReviewId)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewOnDoctorExists(review.ReviewId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(review);
        }

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