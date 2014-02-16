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
    public class SourceController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/Source
        public IEnumerable<Source> GetSources()
        {
            var sources = db.Sources.Include(s => s.SourceType);
            return sources.AsEnumerable();
        }

        // GET api/Source/5
        public Source GetSource(int id)
        {
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return source;
        }

        // PUT api/Source/5
        public HttpResponseMessage PutSource(int id, Source source)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != source.SourceId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(source).State = EntityState.Modified;

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

        // POST api/Source
        public HttpResponseMessage PostSource(Source source)
        {
            if (ModelState.IsValid)
            {
                db.Sources.Add(source);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, source);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = source.SourceId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Source/5
        public HttpResponseMessage DeleteSource(int id)
        {
            Source source = db.Sources.Find(id);
            if (source == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Sources.Remove(source);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, source);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}