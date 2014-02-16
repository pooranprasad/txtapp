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
    public class SourceTypeController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/SourceType
        public IQueryable<SourceType> GetSourceTypes()
        {
            return db.SourceTypes;
        }

        // GET api/SourceType/5
        [ResponseType(typeof(SourceType))]
        public IHttpActionResult GetSourceType(int id)
        {
            SourceType sourcetype = db.SourceTypes.Find(id);
            if (sourcetype == null)
            {
                return NotFound();
            }

            return Ok(sourcetype);
        }

        // PUT api/SourceType/5
        public IHttpActionResult PutSourceType(int id, SourceType sourcetype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sourcetype.SourceTypeId)
            {
                return BadRequest();
            }

            db.Entry(sourcetype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SourceTypeExists(id))
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

        // POST api/SourceType
        [ResponseType(typeof(SourceType))]
        public IHttpActionResult PostSourceType(SourceType sourcetype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SourceTypes.Add(sourcetype);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sourcetype.SourceTypeId }, sourcetype);
        }

        // DELETE api/SourceType/5
        [ResponseType(typeof(SourceType))]
        public IHttpActionResult DeleteSourceType(int id)
        {
            SourceType sourcetype = db.SourceTypes.Find(id);
            if (sourcetype == null)
            {
                return NotFound();
            }

            db.SourceTypes.Remove(sourcetype);
            db.SaveChanges();

            return Ok(sourcetype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SourceTypeExists(int id)
        {
            return db.SourceTypes.Count(e => e.SourceTypeId == id) > 0;
        }
    }
}