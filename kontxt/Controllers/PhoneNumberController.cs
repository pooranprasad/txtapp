using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using kontxt.Models;

namespace kontxt.Controllers
{
    public class PhoneNumberController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/PhoneNumber
        public IQueryable<PhoneNumber> GetPhoneNumbers()
        {
            return db.PhoneNumbers;
        }

        // GET api/PhoneNumber/5
        [ResponseType(typeof(PhoneNumber))]
        public IHttpActionResult GetPhoneNumber(Guid id)
        {
            PhoneNumber phonenumber = db.PhoneNumbers.Find(id);
            if (phonenumber == null)
            {
                return NotFound();
            }

            return Ok(phonenumber);
        }

        // PUT api/PhoneNumber/5
        public IHttpActionResult PutPhoneNumber(Guid id, PhoneNumber phonenumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phonenumber.PhoneNumberId)
            {
                return BadRequest();
            }

            db.Entry(phonenumber).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/PhoneNumber
        [ResponseType(typeof(PhoneNumber))]
        public IHttpActionResult PostPhoneNumber(PhoneNumber phonenumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PhoneNumbers.Add(phonenumber);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PhoneNumberExists(phonenumber.PhoneNumberId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = phonenumber.PhoneNumberId }, phonenumber);
        }

        // DELETE api/PhoneNumber/5
        [ResponseType(typeof(PhoneNumber))]
        public IHttpActionResult DeletePhoneNumber(Guid id)
        {
            PhoneNumber phonenumber = db.PhoneNumbers.Find(id);
            if (phonenumber == null)
            {
                return NotFound();
            }

            db.PhoneNumbers.Remove(phonenumber);
            db.SaveChanges();

            return Ok(phonenumber);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhoneNumberExists(Guid id)
        {
            return db.PhoneNumbers.Count(e => e.PhoneNumberId == id) > 0;
        }
    }
}