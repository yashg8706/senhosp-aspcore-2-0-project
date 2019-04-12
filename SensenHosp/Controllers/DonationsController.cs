using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SensenHosp.Data;
using SensenHosp.Models;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using BraintreeHttp;
using System.Diagnostics;
using Microsoft.AspNetCore;

namespace SensenHosp.Controllers
{
    public class DonationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonationsController(ApplicationDbContext context)
        {
            _context = context;
            //CreateOrder(true).Wait();
            //GetOrder("7BY92840W79868215", true).Wait();
        }

        // GET: Donations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Donations.ToListAsync());
        }

        // GET: Donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations
                .SingleOrDefaultAsync(m => m.ID == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DonorName,DonorEmail,Amount")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donation);
        }

        // GET: Donations/Donate
        public IActionResult Donate()
        {
            return View();
        }

        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations.SingleOrDefaultAsync(m => m.ID == id);
            if (donation == null)
            {
                return NotFound();
            }
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DonorName,DonorEmail,Amount")] Donation donation)
        {
            if (id != donation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationExists(donation.ID))
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
            return View(donation);
        }

        // GET: Donations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations
                .SingleOrDefaultAsync(m => m.ID == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donations.SingleOrDefaultAsync(m => m.ID == id);
            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationExists(int id)
        {
            return _context.Donations.Any(e => e.ID == id);
        }




        // Paypal donation functionality
        // POST: Donations/CreateOrder
        [HttpPost, ActionName("CreateOrder")]
        public async Task<object> CreateOrder([FromBody] dynamic OrderAmount, bool debug = true)
        {
            var oa = (string)OrderAmount["OrderAmount"];
            var request = new OrdersCreateRequest();
            request.Headers.Add("prefer", "return=representation");
            request.RequestBody(BuildRequestBody(oa));

            var response = await PayPalClient.client().Execute(request);
            var result = response.Result<Order>();
            response.Headers.Add("id", result.Id);
            if (debug)
            {
                Debug.WriteLine("Status: {0}", result.Status);
                Debug.WriteLine("Order Id: {0}", result.Id);
                Debug.WriteLine("Intent: {0}", result.Intent);
                Debug.WriteLine("Links:");
                foreach (LinkDescription link in result.Links)
                {
                    Debug.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
                }
            }
            return response;
        }

        private OrderRequest BuildRequestBody(string oa)
        {
            OrderRequest orderRequest = new OrderRequest()
            {
                Intent = "CAPTURE",

                ApplicationContext = new ApplicationContext
                {
                    BrandName = "SENSENHUMBER HOSPITAL",
                    LandingPage = "LOGIN",
                    UserAction = "CONTINUE",
                    ShippingPreference = "NO_SHIPPING"
                },

                PurchaseUnits = new List<PurchaseUnitRequest>
                {
                    new PurchaseUnitRequest
                    {
                        ReferenceId = "HospDonat",
                        Description = "Donation",
                        Amount = new AmountWithBreakdown
                        {
                            CurrencyCode = "USD",
                            Value = oa
                        }
                    }
                }
            };
            Debug.WriteLine(orderRequest.PurchaseUnits);
            return orderRequest;
        }

        [HttpPost]
        //[Route("Donations/CaptureOrder")]
        public async Task<HttpResponse> CaptureOrder([FromBody] dynamic OrderId, bool debug = true)
        {
            var oi = (string)OrderId["OrderId"];
            var request = new OrdersCaptureRequest(oi);
            request.Prefer("return=representation");
            request.RequestBody(new OrderActionRequest());
            //3. Call PayPal to capture an order
            var response = await PayPalClient.client().Execute(request);
            var result = response.Result<Order>();
            response.Headers.Add("DonorName", result.Payer.Name.GivenName + " " + result.Payer.Name.Surname);
            //4. Save the capture ID to your database. Implement logic to save capture to your database for future reference.
            if (debug)
            {
                Debug.WriteLine("Status: {0}", result.Status);
                Debug.WriteLine("Order Id: {0}", result.Id);
                Debug.WriteLine("Intent: {0}", result.Intent);
                Debug.WriteLine("Links:");
                foreach (LinkDescription link in result.Links)
                {
                    Debug.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
                }
                Debug.WriteLine("Capture Ids: ");
                foreach (PurchaseUnit purchaseUnit in result.PurchaseUnits)
                {
                    foreach (Capture capture in purchaseUnit.Payments.Captures)
                    {
                        Debug.WriteLine("\t {0}", capture.Id);
                    }
                }
                AmountWithBreakdown amount = result.PurchaseUnits[0].Amount;
                Debug.WriteLine("Buyer:");
                Debug.WriteLine("\tEmail Address: {0}\n\tName: {1}\n\tPhone Number: {2}{3}", result.Payer.EmailAddress, result.Payer.Name.GivenName + " " + result.Payer.Name.Surname, result.Payer.Phone.CountryCode, result.Payer.Phone.NationalNumber);
            }

            Donation donation = new Donation();

            donation.DonorName = (string)(result.Payer.Name.GivenName + " " + result.Payer.Name.Surname);
            donation.DonorEmail = result.Payer.EmailAddress;
            donation.Amount = result.PurchaseUnits[0].Amount.Value;
            donation.PayPalOrderId = result.Id;

            if (ModelState.IsValid)
            {
                _context.Add(donation);
                await _context.SaveChangesAsync();
            }


            return response;
        }
    }
}
