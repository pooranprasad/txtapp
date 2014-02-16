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
    public class AppUserSourceController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/AppUserSource
        public IQueryable<AppUserSource> GetAppUserSources()
        {
            return db.AppUserSources;
        }

        // GET api/AppUserSource/5
        [ResponseType(typeof(AppUserSource))]
        public IHttpActionResult GetAppUserSource(Guid id)
        {
            AppUserSource appusersource = db.AppUserSources.Find(id);
            if (appusersource == null)
            {
                return NotFound();
            }

            return Ok(appusersource);
        }

        // PUT api/AppUserSource/5
        public IHttpActionResult PutAppUserSource(Guid id, AppUserSource appusersource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appusersource.AppUserSourceId)
            {
                return BadRequest();
            }

            db.Entry(appusersource).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserSourceExists(id))
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

        // POST api/AppUserSource
        [ResponseType(typeof(AppUserSource))]
        public IHttpActionResult PostAppUserSource(AppUserSource appusersource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppUserSources.Add(appusersource);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AppUserSourceExists(appusersource.AppUserSourceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = appusersource.AppUserSourceId }, appusersource);
        }

        // DELETE api/AppUserSource/5
        [ResponseType(typeof(AppUserSource))]
        public IHttpActionResult DeleteAppUserSource(Guid id)
        {
            AppUserSource appusersource = db.AppUserSources.Find(id);
            if (appusersource == null)
            {
                return NotFound();
            }

            db.AppUserSources.Remove(appusersource);
            db.SaveChanges();

            return Ok(appusersource);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppUserSourceExists(Guid id)
        {
            return db.AppUserSources.Count(e => e.AppUserSourceId == id) > 0;
        }
    }
}