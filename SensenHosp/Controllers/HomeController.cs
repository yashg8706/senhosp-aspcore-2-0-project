using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SensenHosp.Data;
using Microsoft.EntityFrameworkCore;
using SensenHosp.Models;
using SensenHosp.Models.ViewModels;

namespace SensenHosp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var posts = _context.BlogPosts
                .Include(a => a.BlogCategory)
                .Skip(0)
                .Take(3)
                .ToList();

            ViewData["testimonial"] = new SelectList(_context.Testimonials, "ID", "Description", "Username", "IsPublished");

            return View(posts);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
