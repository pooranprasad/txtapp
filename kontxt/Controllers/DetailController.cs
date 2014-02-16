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
    public class DetailController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/Detail
        public IEnumerable<Detail> GetDetails()
        {
            var details = db.Details.Include(d => d.DetailType);
            return details.AsEnumerable();
        }

        // GET api/Detail/5
        public Detail GetDetail(int id)
        {
            Detail detail = db.Details.Find(id);
            if (detail == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return detail;
        }

        // PUT api/Detail/5
        public HttpResponseMessage PutDetail(int id, Detail detail)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != detail.DetailId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(detail).State = EntityState.Modified;

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

        // POST api/Detail
        public HttpResponseMessage PostDetail(Detail detail)
        {
            if (ModelState.IsValid)
            {
                db.Details.Add(detail);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, detail);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = detail.DetailId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Detail/5
        public HttpResponseMessage DeleteDetail(int id)
        {
            Detail detail = db.Details.Find(id);
            if (detail == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Details.Remove(detail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, detail);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}