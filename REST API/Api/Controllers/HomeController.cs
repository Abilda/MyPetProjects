using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        DatabaseFirstContext db;
        public HomeController(DatabaseFirstContext context)
        {
            db = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialization>>> Specialization()
        {
            return await db.Specialization.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Specialization>> Specialization(long? id)
        {
            Specialization spec = await db.Specialization.FirstOrDefaultAsync(x => x.Id == id);
            if (spec == null)
                return NotFound();
            return new ObjectResult(spec);
        }
        [HttpPost]
        public async Task<ActionResult<Specialization>> CreateSpecialization(Specialization specialization)
        {
            specialization.Created = DateTime.Now;
            if (specialization == null)
                return BadRequest();
            db.Add(specialization);
            await db.SaveChangesAsync();
            return Ok(specialization);
        }
        [HttpPut]
        public async Task<ActionResult<Specialization>> EditSpecialization(Specialization specialization)
        {
            specialization.Created = DateTime.Now;
            if (specialization == null)
                return BadRequest();
            if (!db.Specialization.Any(x => x.Id == specialization.Id))
                return NotFound();
            db.Update(specialization);
            await db.SaveChangesAsync();
            return Ok(specialization);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Specialization>> DeleteSpecialization(long? id)
        {
            Specialization spec = db.Specialization.FirstOrDefault(x => x.Id == id);
            if (spec == null)
            {
                return NotFound();
            }
            db.Specialization.Remove(spec);
            await db.SaveChangesAsync();
            return Ok(spec);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctors>>> Doctors()
        {
            return await db.Doctors.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctors>> Doctors(long? id)
        {
            Doctors doc = await db.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            if (doc == null)
                return NotFound();
            return new ObjectResult(doc);
        }
        [HttpPost]
        public async Task<ActionResult<Doctors>> CreateDoctor(Doctors doc)
        {
            doc.Created = DateTime.Now;
            if (doc == null)
                return BadRequest();
            await db.SaveChangesAsync();
            return Ok(doc);
        }
        [HttpPut]
        public async Task<ActionResult<Doctors>> EditDoctor(Doctors doc)
        {
            doc.Created = DateTime.Now;
            if (doc == null)
                return BadRequest();
            if (!db.Specialization.Any(x => x.Id == doc.Id))
                return NotFound();
            db.Update(doc);
            await db.SaveChangesAsync();
            return Ok(doc);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Doctors>> DeleteDoctor(long? id)
        {
            Doctors doc = db.Doctors.FirstOrDefault(x => x.Id == id);
            if (doc == null)
            {
                return NotFound();
            }
            db.Doctors.Remove(doc);
            await db.SaveChangesAsync();
            return Ok(doc);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patients>>> Patients()
        {
            return await db.Patients.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Patients>> Patients(long? id)
        {
            Patients pat = await db.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (pat == null)
                return NotFound();
            return new ObjectResult(pat);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patientdoctors>>> PatientDoctors()
        {
            return await db.Patientdoctors.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Patientdoctors>> PatientDoctors(long? id)
        {
            Patientdoctors appointment = await db.Patientdoctors.FirstOrDefaultAsync(x=>x.Id==id);
            if (appointment == null)
                return NotFound();
            return new ObjectResult(appointment);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conclusions>>> Conclusions()
        {
            return await db.Conclusions.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Conclusions>> Conclusions(long? id)
        {
            Conclusions con = await db.Conclusions.FirstOrDefaultAsync(x => x.Id == id);
            if (con == null)
                return NotFound();
            return new ObjectResult(con);
        }
    }
}