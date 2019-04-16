using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SensenHosp.Data;
using SensenHosp.Models;

namespace SensenHosp.Controllers
{
    public class PhysiciansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhysiciansController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        //GET: Physicians/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("physicianId,physicianName,scheduleStartTime,scheduleEndTime")] Physician physician)
        {
            if (ModelState.IsValid)
            {
                _context.Add(physician);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(physician);
        }
    }
}