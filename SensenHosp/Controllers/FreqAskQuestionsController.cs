using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SensenHosp.Data;
using SensenHosp.Models;

//IM STILL ON THE PROCESS OF FIGURING THINGS OUT, I JUST DID THE CRUD BASICS FOR MY FEATURE
//THE PUBLIC PAGE WILL HAVE THE LIST OF FAQS THE ONE I MADE FOR THE VIEW RIGHT NOW WAS FRO THE ADMIN.
//THE PUBLIC PAGE WILL HAVE ACCORDION FOR THE LIST OF FAQS.
namespace SensenHosp.Controllers
{
    public class FreqAskQuestionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FreqAskQuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FreqAskQuestions
        public async Task<IActionResult> Index(int pagenum)
        {
            //THIS PAGINATION IS FOR THE INDEX WHERE THE USERS AND VISITOR CAN SEE THE LIST OF THE FAQS STORED FROM THE DATABASE AND IT WILL BE
            //AN ACCORDION WHERE IT WILL HIDE AND SHOW ANSWER WHEN THE USER CLICKS ON THE QUESTION

            var _faq = await _context.FreqAskQuestion.ToListAsync();

            int faqCount = _faq.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)faqCount / perpage) - 1;

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
            else
            {
                ViewData["PaginationSummary"] = "Page 1 of 1";
            }

            List<FreqAskQuestion> freqAskQuestion = await _context.FreqAskQuestion.Skip(start).Take(perpage).ToListAsync();
            return View(freqAskQuestion);
            //return View(await _context.FreqAskQuestion.ToListAsync());
        }
        public async Task<IActionResult> Admin(int pagenum)
        {
            var _faq = await _context.FreqAskQuestion.ToListAsync();

            int faqCount = _faq.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)faqCount / perpage) - 1;

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
            else
            {
                ViewData["PaginationSummary"] = "Page 1 of 1";
            }

            List<FreqAskQuestion> freqAskQuestion = await _context.FreqAskQuestion.Skip(start).Take(perpage).ToListAsync();
            return View(freqAskQuestion);
            //return View(await _context.FreqAskQuestion.ToListAsync());
        }

        // GET: FreqAskQuestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freqAskQuestion = await _context.FreqAskQuestion
                .SingleOrDefaultAsync(m => m.ID == id);
            if (freqAskQuestion == null)
            {
                return NotFound();
            }

            return View(freqAskQuestion);
        }

        // GET: FreqAskQuestions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FreqAskQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Question,Answer,DateCreated")] FreqAskQuestion freqAskQuestion)
        {
            if (ModelState.IsValid)
            {
                freqAskQuestion.DateCreated = DateTime.Now;
                _context.Add(freqAskQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Admin));
            }
            return View(freqAskQuestion);
        }

        // GET: FreqAskQuestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freqAskQuestion = await _context.FreqAskQuestion.SingleOrDefaultAsync(m => m.ID == id);
            if (freqAskQuestion == null)
            {
                return NotFound();
            }
            return View(freqAskQuestion);
        }

        // POST: FreqAskQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Question,Answer,DateModified")] FreqAskQuestion freqAskQuestion)
        {
            if (id != freqAskQuestion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    freqAskQuestion.DateModified = DateTime.Now;
                    _context.Update(freqAskQuestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FreqAskQuestionExists(freqAskQuestion.ID))
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
            return View(freqAskQuestion);
        }

        // GET: FreqAskQuestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var freqAskQuestion = await _context.FreqAskQuestion
                .SingleOrDefaultAsync(m => m.ID == id);
            if (freqAskQuestion == null)
            {
                return NotFound();
            }

            return View(freqAskQuestion);
        }

        // POST: FreqAskQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var freqAskQuestion = await _context.FreqAskQuestion.SingleOrDefaultAsync(m => m.ID == id);
            _context.FreqAskQuestion.Remove(freqAskQuestion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Admin));
        }

        private bool FreqAskQuestionExists(int id)
        {
            return _context.FreqAskQuestion.Any(e => e.ID == id);
        }
    }
}
