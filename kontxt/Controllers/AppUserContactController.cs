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
    public class AppUserContactController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/AppUserContact
        public IQueryable<AppUserContact> GetAppUserContacts()
        {
            return db.AppUserContacts;
        }

        // GET api/AppUserContact/5
        [ResponseType(typeof(AppUserContact))]
        public IHttpActionResult GetAppUserContact(Guid id)
        {
            AppUserContact appusercontact = db.AppUserContacts.Find(id);
            if (appusercontact == null)
            {
                return NotFound();
            }

            return Ok(appusercontact);
        }

        // PUT api/AppUserContact/5
        public IHttpActionResult PutAppUserContact(Guid id, AppUserContact appusercontact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appusercontact.AppUserContactId)
            {
                return BadRequest();
            }

            db.Entry(appusercontact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserContactExists(id))
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

        // POST api/AppUserContact
        [ResponseType(typeof(AppUserContact))]
        public IHttpActionResult PostAppUserContact(AppUserContact appusercontact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppUserContacts.Add(appusercontact);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AppUserContactExists(appusercontact.AppUserContactId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = appusercontact.AppUserContactId }, appusercontact);
        }

        // DELETE api/AppUserContact/5
        [ResponseType(typeof(AppUserContact))]
        public IHttpActionResult DeleteAppUserContact(Guid id)
        {
            AppUserContact appusercontact = db.AppUserContacts.Find(id);
            if (appusercontact == null)
            {
                return NotFound();
            }

            db.AppUserContacts.Remove(appusercontact);
            db.SaveChanges();

            return Ok(appusercontact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppUserContactExists(Guid id)
        {
            return db.AppUserContacts.Count(e => e.AppUserContactId == id) > 0;
        }
    }
}