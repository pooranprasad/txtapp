using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using kontxt.Models;

namespace kontxt.Controllers
{
    public class SourceTypeController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/SourceType
        public IEnumerable<SourceType> GetSourceTypes()
        {
            return db.SourceTypes.AsEnumerable();
        }

        // GET api/SourceType/5
        public SourceType GetSourceType(int id)
        {
            SourceType sourcetype = db.SourceTypes.Find(id);
            if (sourcetype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return sourcetype;
        }

        // PUT api/SourceType/5
        public HttpResponseMessage PutSourceType(int id, SourceType sourcetype)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != sourcetype.SourceTypeId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(sourcetype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/SourceType
        public HttpResponseMessage PostSourceType(SourceType sourcetype)
        {
            if (ModelState.IsValid)
            {
                db.SourceTypes.Add(sourcetype);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, sourcetype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = sourcetype.SourceTypeId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/SourceType/5
        public HttpResponseMessage DeleteSourceType(int id)
        {
            SourceType sourcetype = db.SourceTypes.Find(id);
            if (sourcetype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.SourceTypes.Remove(sourcetype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, sourcetype);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}