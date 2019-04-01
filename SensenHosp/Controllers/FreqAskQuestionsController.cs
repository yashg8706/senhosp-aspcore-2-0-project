using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SensenHosp.Data;
using SensenHosp.Models;

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
        public async Task<IActionResult> Index()
        {
            return View(await _context.FreqAskQuestion.ToListAsync());
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
                _context.Add(freqAskQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        private bool FreqAskQuestionExists(int id)
        {
            return _context.FreqAskQuestion.Any(e => e.ID == id);
        }
    }
}
