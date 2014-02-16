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
    public class EmailAddressController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/EmailAddress
        public IQueryable<EmailAddress> GetEmailAddresses()
        {
            return db.EmailAddresses;
        }

        // GET api/EmailAddress/5
        [ResponseType(typeof(EmailAddress))]
        public IHttpActionResult GetEmailAddress(Guid id)
        {
            EmailAddress emailaddress = db.EmailAddresses.Find(id);
            if (emailaddress == null)
            {
                return NotFound();
            }

            return Ok(emailaddress);
        }

        // PUT api/EmailAddress/5
        public IHttpActionResult PutEmailAddress(Guid id, EmailAddress emailaddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emailaddress.EmailAddressId)
            {
                return BadRequest();
            }

            db.Entry(emailaddress).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailAddressExists(id))
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

        // POST api/EmailAddress
        [ResponseType(typeof(EmailAddress))]
        public IHttpActionResult PostEmailAddress(EmailAddress emailaddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmailAddresses.Add(emailaddress);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmailAddressExists(emailaddress.EmailAddressId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = emailaddress.EmailAddressId }, emailaddress);
        }

        // DELETE api/EmailAddress/5
        [ResponseType(typeof(EmailAddress))]
        public IHttpActionResult DeleteEmailAddress(Guid id)
        {
            EmailAddress emailaddress = db.EmailAddresses.Find(id);
            if (emailaddress == null)
            {
                return NotFound();
            }

            db.EmailAddresses.Remove(emailaddress);
            db.SaveChanges();

            return Ok(emailaddress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmailAddressExists(Guid id)
        {
            return db.EmailAddresses.Count(e => e.EmailAddressId == id) > 0;
        }
    }
}