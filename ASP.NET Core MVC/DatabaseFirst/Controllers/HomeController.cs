using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DatabaseFirst.Models; 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DatabaseFirst.Models.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        DatabaseFirstContext db;
        public HomeController(ILogger<HomeController> logger, DatabaseFirstContext context)
        {
            db = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Specialization()
        {
            return View(db.Specialization.FromSqlRaw("Select * from Specialization where Deleted is NULL")
                .ToList());
        }
        public IActionResult CreateSpecialization()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpecialization(Specialization spec)
        {
            db.Specialization.Add(spec);
            spec.Created = DateTime.Now;
            await db.SaveChangesAsync();
            return RedirectToAction("Specialization");
        }
        public async Task<IActionResult> EditSpecialization(long? Id)
        {
            if (Id != null)
            {
                Specialization spec = await db.Specialization.FirstOrDefaultAsync(p => p.Id == Id);
                if (spec != null)
                {
                    return View(spec);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditSpecialization(Specialization spec)
        {
            spec.Created = DateTime.Now;
            db.Specialization.Update(spec);
            await db.SaveChangesAsync();
            return RedirectToAction("Specialization");
        }
        [HttpGet]
        [ActionName("DeleteSpecialization")]
        public async Task<IActionResult> ConfirmDeleteSpecialization (int? id)
        {
            if (id != null)
            {
                Specialization spec = await db.Specialization.FirstOrDefaultAsync(p => p.Id == id);
                if (spec != null)
                    return View(spec);
            }
            return NotFound();
        }
        public async Task<IActionResult> DeleteSpecialization (int? id)
        {
            if (id != null)
            {
                Specialization spec = await db.Specialization.FirstOrDefaultAsync(p => p.Id == id);
                if (spec != null)
                {
                    spec.Deleted = DateTime.Now;
                    db.Specialization.Update(spec);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Specialization");
                }
            }
            return NotFound();
        }
        public IActionResult Doctors ()
        {
            return View(db.Doctors.FromSqlRaw("SELECT * FROM Doctors WHERE deleted IS NULL").
                Include(c=>c.Specialization).ToList());
        }
        public IActionResult CreateDoctor()
        {
            return View("CreateDoctorFirstPart");
        }
        [HttpPost]
        public IActionResult CreateDoctorFirstPart(string FirstName, string FamilyName, 
            string MiddleName)
        {
            Doctors doctor = new Doctors();
            SelectList specializations = new SelectList(db.Specialization.FromSqlRaw("SELECT * FROM " +
                "specialization WHERE deleted IS NULL").ToList(), "Id", "Name");
            ViewBag.Specialization = specializations;
            doctor.Firstname = FirstName;
            doctor.Familyname = FamilyName;
            doctor.Middlename = MiddleName;
            doctor.Created = DateTime.Now;
            return View("CreateDoctorSecondPart", doctor);
        }
        [HttpPost]
        public IActionResult CreateDoctorSecondPart(Doctors doc)
        {
            db.Doctors.Add(doc);
            doc.Created = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Doctors");
        }
        public async Task<IActionResult> EditDoctor(int? id)
        {
            if (id != null)
            {
                Doctors doc = await db.Doctors.FirstOrDefaultAsync(p => p.Id == id);
                if (doc != null)
                {
                    SelectList specializations = new SelectList(db.Specialization.FromSqlRaw("SELECT *" +
                        "FROM specialization WHERE Deleted IS NULL").ToList(), "Id", "Name");
                    ViewBag.Specialization = specializations;
                    return View(doc);
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditDoctor(Doctors doc)
        {
            doc.Created = DateTime.Now;
            db.Doctors.Update(doc);
            await db.SaveChangesAsync();    
            return RedirectToAction("Doctors");
        }
        [HttpGet]
        [ActionName("DeleteDoctor")]
        public async Task<IActionResult> ConfirmDeleteDoctor(int? id)
        {
            if (id != null)
            {
                Doctors doc = await db.Doctors.FirstOrDefaultAsync(p => p.Id == id);
                if (doc != null)
                {
                    return View(doc);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteDoctor(int? id)
        {
            if (id != null)
            {
                Doctors doc = await db.Doctors.FirstOrDefaultAsync(p => p.Id == id);
                if (doc != null)
                {
                    doc.Deleted = DateTime.Now;
                    db.Doctors.Update(doc);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Doctors");
                }
            }
            return NotFound();
        }
        public IActionResult Patients()
        {
            return View(db.Patients.FromSqlRaw("SELECT * FROM Patients where deleted IS NULL")
                .ToList());
        }
        public IActionResult CreatePatient()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePatient(Patients someone)
        {
            someone.Created = DateTime.Now;
            db.Patients.Add(someone);
            await db.SaveChangesAsync();
            return RedirectToAction("Patients");
        }
        public IActionResult EditPatient(int? id)
        {
            if (id != null)
            {
                Patients someone = db.Patients.FirstOrDefault(p => p.Id == id);
                if (someone != null)
                    return View(someone);
            }
            return NotFound() ;
        }
        [HttpPost]
        public async Task<IActionResult> EditPatient(Patients someone)
        {
            someone.Created = DateTime.Now;
            db.Patients.Update(someone);
            await db.SaveChangesAsync();
            return RedirectToAction("Patients");
        }
        [HttpGet]
        [ActionName("DeletePatient")]
        public async Task<IActionResult> ConfirmDeletePatient(int? id)
        {
            if (id != null)
            {
                Patients someone = await db.Patients.FirstOrDefaultAsync(p => p.Id == id);
                if (someone != null)
                    return View(someone);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeletePatient(int? id)
        {
            if (id != null)
            {
                Patients someone = db.Patients.FirstOrDefault(p => p.Id == id);
                someone.Deleted = DateTime.Now;
                db.Patients.Update(someone);
                await db.SaveChangesAsync();
                return RedirectToAction("Patients");
            }
            return NotFound();
        }
        public IActionResult AboutPatient(int? id)
        {  
            if(id != null)
            {
                Patients someone = db.Patients.FirstOrDefault(p => p.Id == id);
                return View(someone);
            }
            return NotFound(); 
        }
        public IActionResult CreateAppointment(int? id)
        {
            if (id != null)
            {
                Patients someone = db.Patients.FirstOrDefault(p => p.Id == id);
                var listofdoctors = db.Doctors.FromSqlRaw("SELECT * FROM doctors WHERE deleted IS NULL")
                    .Include(c=>c.Specialization).ToList();
                ViewBag.ID = id;
                string fullname = someone.Familyname +" " + someone.Firstname + " " + someone.Middlename;
                ViewBag.FIO = fullname;
                return View(listofdoctors);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(long Doctorid, DateTime VisitDate, long id)
        {
            Patientdoctors appointment = new Patientdoctors();
            appointment.Doctorid = Doctorid;
            appointment.Visitdate = VisitDate; 
            appointment.Created = DateTime.Now;
            appointment.Patientid = id;
            db.Patientdoctors.Add(appointment);
            await db.SaveChangesAsync();
            return RedirectToAction("AboutPatient", "Home", id) ;
        }
        public IActionResult AboutDoctor(int? id)
        {
            if (id!=null)
            {
                var listofPatients = db.Doctors.FromSqlRaw("SELECT * from Doctors WHERE deleted is NOT NULL")
                    .Include(c => c.Patientdoctors).ToList();
                Doctors doc = db.Doctors.Include(c=>c.Specialization).FirstOrDefault(c => c.Id == id);
                return View(doc);
            }
            return NotFound();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}