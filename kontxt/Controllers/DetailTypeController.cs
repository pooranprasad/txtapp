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
    public class DetailTypeController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/DetailType
        public IQueryable<DetailType> GetDetailTypes()
        {
            return db.DetailTypes;
        }

        // GET api/DetailType/5
        [ResponseType(typeof(DetailType))]
        public IHttpActionResult GetDetailType(int id)
        {
            DetailType detailtype = db.DetailTypes.Find(id);
            if (detailtype == null)
            {
                return NotFound();
            }

            return Ok(detailtype);
        }

        // PUT api/DetailType/5
        public IHttpActionResult PutDetailType(int id, DetailType detailtype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detailtype.DetailTypeId)
            {
                return BadRequest();
            }

            db.Entry(detailtype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailTypeExists(id))
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

        // POST api/DetailType
        [ResponseType(typeof(DetailType))]
        public IHttpActionResult PostDetailType(DetailType detailtype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DetailTypes.Add(detailtype);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = detailtype.DetailTypeId }, detailtype);
        }

        // DELETE api/DetailType/5
        [ResponseType(typeof(DetailType))]
        public IHttpActionResult DeleteDetailType(int id)
        {
            DetailType detailtype = db.DetailTypes.Find(id);
            if (detailtype == null)
            {
                return NotFound();
            }

            db.DetailTypes.Remove(detailtype);
            db.SaveChanges();

            return Ok(detailtype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetailTypeExists(int id)
        {
            return db.DetailTypes.Count(e => e.DetailTypeId == id) > 0;
        }
    }
}