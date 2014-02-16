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
    public class ContactEmailAddressController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/ContactEmailAddress
        public IQueryable<ContactEmailAddress> GetContactEmailAddresses()
        {
            return db.ContactEmailAddresses;
        }

        // GET api/ContactEmailAddress/5
        [ResponseType(typeof(ContactEmailAddress))]
        public IHttpActionResult GetContactEmailAddress(Guid id)
        {
            ContactEmailAddress contactemailaddress = db.ContactEmailAddresses.Find(id);
            if (contactemailaddress == null)
            {
                return NotFound();
            }

            return Ok(contactemailaddress);
        }

        // PUT api/ContactEmailAddress/5
        public IHttpActionResult PutContactEmailAddress(Guid id, ContactEmailAddress contactemailaddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactemailaddress.ContactEmailAddressId)
            {
                return BadRequest();
            }

            db.Entry(contactemailaddress).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactEmailAddressExists(id))
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

        // POST api/ContactEmailAddress
        [ResponseType(typeof(ContactEmailAddress))]
        public IHttpActionResult PostContactEmailAddress(ContactEmailAddress contactemailaddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactEmailAddresses.Add(contactemailaddress);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ContactEmailAddressExists(contactemailaddress.ContactEmailAddressId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = contactemailaddress.ContactEmailAddressId }, contactemailaddress);
        }

        // DELETE api/ContactEmailAddress/5
        [ResponseType(typeof(ContactEmailAddress))]
        public IHttpActionResult DeleteContactEmailAddress(Guid id)
        {
            ContactEmailAddress contactemailaddress = db.ContactEmailAddresses.Find(id);
            if (contactemailaddress == null)
            {
                return NotFound();
            }

            db.ContactEmailAddresses.Remove(contactemailaddress);
            db.SaveChanges();

            return Ok(contactemailaddress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactEmailAddressExists(Guid id)
        {
            return db.ContactEmailAddresses.Count(e => e.ContactEmailAddressId == id) > 0;
        }
    }
}