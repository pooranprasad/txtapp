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
    public class ContactPhoneNumberController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/ContactPhoneNumber
        public IQueryable<ContactPhoneNumber> GetContactPhoneNumbers()
        {
            return db.ContactPhoneNumbers;
        }

        // GET api/ContactPhoneNumber/5
        [ResponseType(typeof(ContactPhoneNumber))]
        public IHttpActionResult GetContactPhoneNumber(Guid id)
        {
            ContactPhoneNumber contactphonenumber = db.ContactPhoneNumbers.Find(id);
            if (contactphonenumber == null)
            {
                return NotFound();
            }

            return Ok(contactphonenumber);
        }

        // PUT api/ContactPhoneNumber/5
        public IHttpActionResult PutContactPhoneNumber(Guid id, ContactPhoneNumber contactphonenumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactphonenumber.ContactPhoneNumberId)
            {
                return BadRequest();
            }

            db.Entry(contactphonenumber).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactPhoneNumberExists(id))
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

        // POST api/ContactPhoneNumber
        [ResponseType(typeof(ContactPhoneNumber))]
        public IHttpActionResult PostContactPhoneNumber(ContactPhoneNumber contactphonenumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactPhoneNumbers.Add(contactphonenumber);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ContactPhoneNumberExists(contactphonenumber.ContactPhoneNumberId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = contactphonenumber.ContactPhoneNumberId }, contactphonenumber);
        }

        // DELETE api/ContactPhoneNumber/5
        [ResponseType(typeof(ContactPhoneNumber))]
        public IHttpActionResult DeleteContactPhoneNumber(Guid id)
        {
            ContactPhoneNumber contactphonenumber = db.ContactPhoneNumbers.Find(id);
            if (contactphonenumber == null)
            {
                return NotFound();
            }

            db.ContactPhoneNumbers.Remove(contactphonenumber);
            db.SaveChanges();

            return Ok(contactphonenumber);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactPhoneNumberExists(Guid id)
        {
            return db.ContactPhoneNumbers.Count(e => e.ContactPhoneNumberId == id) > 0;
        }
    }
}