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
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET: Appointments/Create
        public IActionResult Create()
        {
            return View();
        }
        //POST: Appointments/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(string FirstName, string MiddleName, string LastName, string EmailId, string MobileNo, string Description, string DoctorName, DateTime AppointmentDate)
        {
            string query = "insert into appointments (FirstName,MiddleName, LastName, EmailId, MobileNo, Description, DoctorName, AppointmentDate , IsConfirmed, CreatedOn)" +
                "values (@firstname,@middlename, @lastname, @emailid, @mobileno, @description, @doctorname, @appointmentdate, 0, getdate())";

            SqlParameter[] myparams = new SqlParameter[8];

            myparams[0] = new SqlParameter("@firstname", FirstName);
            myparams[1] = new SqlParameter("@middlename", MiddleName);
            myparams[2] = new SqlParameter("@lastname", LastName);
            myparams[3] = new SqlParameter("@emailid", EmailId);
            myparams[4] = new SqlParameter("@mobileno", MobileNo);
            myparams[5] = new SqlParameter("@description", Description);
            myparams[6] = new SqlParameter("@doctorname", DoctorName);
            myparams[7] = new SqlParameter("@appointmentdate", AppointmentDate);

            _context.Database.ExecuteSqlCommand(query, myparams);
            return RedirectToAction("Create");
        }

        public IActionResult List()
        {
            return View(_context.Appointments.ToList());

        }

        //GET: Appointment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(400);
            }
            Appointment appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        //POST: Appointment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,string FirstName, string MiddleName, string LastName, string EmailId, string MobileNo, string Description, string DoctorName, DateTime AppointmentDate, string AppointmentTime)
        {
            if (id == null || (_context.Appointments.Find(id) == null))
            {
                return NotFound();
            }
            string query = "update appointments set FirstName = @firstname," +
                "MiddleName = @middlename, " +
                "LastName = @lastname, " +
                "EmailId = @emailid, " +
                "MobileNo = @mobileno, " +
                "Description = @description, " +
                "DoctorName = @doctorname, " +
                "AppointmentDate = @appointmentdate, " +
                "AppointmentTime = @appointmenttime " +
                "where ID = @id";

            SqlParameter[] myparams = new SqlParameter[10];

            myparams[0] = new SqlParameter("@id", id);
            myparams[1] = new SqlParameter("@firstname", FirstName);
            myparams[2] = new SqlParameter("@middlename", MiddleName);
            myparams[3] = new SqlParameter("@lastname", LastName);
            myparams[4] = new SqlParameter("@emailid", EmailId);
            myparams[5] = new SqlParameter("@mobileno", MobileNo);
            myparams[6] = new SqlParameter("@description", Description);
            myparams[7] = new SqlParameter("@doctorname", DoctorName);
            myparams[8] = new SqlParameter("@appointmentdate", AppointmentDate);
            myparams[9] = new SqlParameter("@appointmenttime", AppointmentTime);

            _context.Database.ExecuteSqlCommand(query, myparams);
            return RedirectToAction("Edit/"+id);
        }
        //GET: Appointment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(400);
            }
            Appointment appointment = _context.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        public ActionResult Delete(int? id)
        {
            if((id ==null)||_context.Appointments.Find(id)==null)
            {
                return NotFound();
            }
            return View();
        }
    }
}