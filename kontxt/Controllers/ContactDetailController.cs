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
    public class ContactDetailController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/ContactDetail
        public IQueryable<ContactDetail> GetContactDetails()
        {
            return db.ContactDetails;
        }

        // GET api/ContactDetail/5
        [ResponseType(typeof(ContactDetail))]
        public IHttpActionResult GetContactDetail(Guid id)
        {
            ContactDetail contactdetail = db.ContactDetails.Find(id);
            if (contactdetail == null)
            {
                return NotFound();
            }

            return Ok(contactdetail);
        }

        // PUT api/ContactDetail/5
        public IHttpActionResult PutContactDetail(Guid id, ContactDetail contactdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactdetail.ContactDetailId)
            {
                return BadRequest();
            }

            db.Entry(contactdetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactDetailExists(id))
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

        // POST api/ContactDetail
        [ResponseType(typeof(ContactDetail))]
        public IHttpActionResult PostContactDetail(ContactDetail contactdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactDetails.Add(contactdetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ContactDetailExists(contactdetail.ContactDetailId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = contactdetail.ContactDetailId }, contactdetail);
        }

        // DELETE api/ContactDetail/5
        [ResponseType(typeof(ContactDetail))]
        public IHttpActionResult DeleteContactDetail(Guid id)
        {
            ContactDetail contactdetail = db.ContactDetails.Find(id);
            if (contactdetail == null)
            {
                return NotFound();
            }

            db.ContactDetails.Remove(contactdetail);
            db.SaveChanges();

            return Ok(contactdetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactDetailExists(Guid id)
        {
            return db.ContactDetails.Count(e => e.ContactDetailId == id) > 0;
        }
    }
}