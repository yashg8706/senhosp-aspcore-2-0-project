using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SensenHosp.Models;
using SensenHosp.Data;
using System.Data.SqlClient;


namespace SensenHosp.Controllers
{
    public class FeedbackOnDoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbackOnDoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: FeedbackOnDoctor/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        //POST: FeedbackOnDoctor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeedbackID,DoctorName,Feedback,Reply")] FeedbackOnDoctor feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create");
            }
            return View(feedback);
        }

        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public ActionResult Create(string DoctorName, string Feedback)
        //{
        //    string query = "insert into FeedbackOnDoctor (DoctorName,Feedback)" +
        //        "values (@doctorname,@feedback)";

        //    SqlParameter[] myparams = new SqlParameter[2];

        //    myparams[0] = new SqlParameter("@doctorname", DoctorName);
        //    myparams[1] = new SqlParameter("@feedback", Feedback);

        //    _context.Database.ExecuteSqlCommand(query, myparams);
        //    return RedirectToAction("Create");
        //}

    }
}