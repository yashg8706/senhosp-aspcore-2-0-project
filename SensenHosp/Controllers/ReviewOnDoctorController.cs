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
                return RedirectToAction(nameof(Create));
            }
            return View(review);
        }
        //GET: Reviews
        public async Task<IActionResult> List()
        {
            return View(await _context.ReviewOnDoctor.ToListAsync());
        }
    }
}