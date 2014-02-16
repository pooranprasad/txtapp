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
    public class ContactLinkController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/ContactLink
        public IQueryable<ContactLink> GetContactLinks()
        {
            return db.ContactLinks;
        }

        // GET api/ContactLink/5
        [ResponseType(typeof(ContactLink))]
        public IHttpActionResult GetContactLink(Guid id)
        {
            ContactLink contactlink = db.ContactLinks.Find(id);
            if (contactlink == null)
            {
                return NotFound();
            }

            return Ok(contactlink);
        }

        // PUT api/ContactLink/5
        public IHttpActionResult PutContactLink(Guid id, ContactLink contactlink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactlink.ContactLinkId)
            {
                return BadRequest();
            }

            db.Entry(contactlink).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactLinkExists(id))
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

        // POST api/ContactLink
        [ResponseType(typeof(ContactLink))]
        public IHttpActionResult PostContactLink(ContactLink contactlink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactLinks.Add(contactlink);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ContactLinkExists(contactlink.ContactLinkId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = contactlink.ContactLinkId }, contactlink);
        }

        // DELETE api/ContactLink/5
        [ResponseType(typeof(ContactLink))]
        public IHttpActionResult DeleteContactLink(Guid id)
        {
            ContactLink contactlink = db.ContactLinks.Find(id);
            if (contactlink == null)
            {
                return NotFound();
            }

            db.ContactLinks.Remove(contactlink);
            db.SaveChanges();

            return Ok(contactlink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactLinkExists(Guid id)
        {
            return db.ContactLinks.Count(e => e.ContactLinkId == id) > 0;
        }
    }
}