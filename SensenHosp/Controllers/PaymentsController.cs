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
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Payments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Payments.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .SingleOrDefaultAsync(m => m.id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,patientFname,patientLname,invoiceId,invoiceDate,amount")] Payments payments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payments);
                await _context.SaveChangesAsync();
                return RedirectToAction("Pay", "Payments");
                //return RedirectToAction(nameof(Index));
            }
            return View(payments);
         
        }

        // GET: Payments/Pay
        public IActionResult Pay()
        {
            return View();
        }


        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.SingleOrDefaultAsync(m => m.id == id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,patientFname,patientLname,invoiceId,invoiceDate,amount")] Payments payments)
        {
            if (id != payments.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentsExists(payments.id))
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
            return View(payments);
        }
        private bool PaymentsExists(int id)
        {
            return _context.Payments.Any(e => e.id == id);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payments = await _context.Payments
                .SingleOrDefaultAsync(m => m.id == id);
            if (payments == null)
            {
                return NotFound();
            }

            return View(payments);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payments = await _context.Payments.SingleOrDefaultAsync(m => m.id == id);
            _context.Payments.Remove(payments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // Paypal payments functionality
        // POST: Payments/CreateOrder
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
                        ReferenceId = "HospPayment",
                        Description = "Bill  Payment",
                        Amount = new AmountWithBreakdown
                        {
                            CurrencyCode = "CAD",
                            Value = oa
                        }
                    }
                }
            };
            Debug.WriteLine(orderRequest.PurchaseUnits);
            return orderRequest;
        }

        [HttpPost]
        //[Route("Payments/CaptureOrder")]
        public async Task<HttpResponse> CaptureOrder([FromBody] dynamic OrderId, bool debug = true)
        {
            var oi = (string)OrderId["OrderId"];
            var request = new OrdersCaptureRequest(oi);
            request.Prefer("return=representation");
            request.RequestBody(new OrderActionRequest());
            //3. Call PayPal to capture an order
            var response = await PayPalClient.client().Execute(request);
            var result = response.Result<Order>();
            response.Headers.Add("Payee Name", result.Payer.Name.GivenName + " " + result.Payer.Name.Surname);
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
            Debug.WriteLine("************************************************************************************** ");
            int last_payment_id = _context.Payments.Max(item => item.id);

            Debug.WriteLine("Buyer:", last_payment_id);
            Payments payments = await _context.Payments.SingleOrDefaultAsync(m => m.id == last_payment_id);
            Debug.WriteLine("************************************************************************************** ");

            payments.amount = result.PurchaseUnits[0].Amount.Value;
            payments.transactionId = result.Id;
            payments.payeeEmail = result.Payer.EmailAddress;

            if (ModelState.IsValid)
            {
                _context.Update(payments);
                await _context.SaveChangesAsync();
            }


            return response;
        }
    }
}