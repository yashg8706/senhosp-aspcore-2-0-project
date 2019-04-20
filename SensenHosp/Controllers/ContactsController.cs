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
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Admin(int pagenum)
        {
            //var countMsg = await _context.Contact.Where(m => m.message_status == false).ToListAsync();

            var _contact = await _context.Contact.ToListAsync();

            int contactCount = _contact.Count();
            int perpage = 3;
            int maxpage = (int)Math.Ceiling((decimal)contactCount / perpage) - 1;

            var msgCount = _contact.Count(m => m.message_status == false);

            //THIS WILL DISPLAY A BOTSTRAP BADGE WHERE IT COUNTS HOW MANY UNREAD MESSAGES CURRENTLY ON DATABASE
            if(msgCount > 0) {

            ViewData["UnreadMessages"] = _contact.Count(m => m.message_status == false);
            }

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

            //THE LIST OF MESSAGES THAT WILL BE DISPLAYED WILL BE IN DESCENDING ORDER SO ADMIN CAN SEE THE MOST RECENT MESSAGES RECEIVED FROM USERS
            List<Contact> contact = await _context.Contact.OrderByDescending(m => m.DateSent).Skip(start).Skip(start).Take(perpage).ToListAsync();
            return View(contact);
        }

        // GET: Contacts/SuccessMesasge
        public IActionResult SuccessMesasge()
        {
            return View();
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .SingleOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FullName,email,phone,Subject,message,DateSent")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.DateSent = DateTime.Now;
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SuccessMesasge));
            }
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.SingleOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FullName,email,phone,Subject,message,message_status")] Contact contact)
        {
            if (id != contact.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    //I STILL DON'T KNOW TO NOT TO TOUCH OTHER FIELDS AND THE DATE
                    contact.DateSent = DateTime.Now;
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.ID))
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
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .SingleOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contact.SingleOrDefaultAsync(m => m.ID == id);
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Admin));
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.ID == id);
        }
    }
}
