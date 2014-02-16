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
    public class DetailTypeController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/DetailType
        public IEnumerable<DetailType> GetDetailTypes()
        {
            return db.DetailTypes.AsEnumerable();
        }

        // GET api/DetailType/5
        public DetailType GetDetailType(int id)
        {
            DetailType detailtype = db.DetailTypes.Find(id);
            if (detailtype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return detailtype;
        }

        // PUT api/DetailType/5
        public HttpResponseMessage PutDetailType(int id, DetailType detailtype)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != detailtype.DetailTypeId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(detailtype).State = EntityState.Modified;

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

        // POST api/DetailType
        public HttpResponseMessage PostDetailType(DetailType detailtype)
        {
            if (ModelState.IsValid)
            {
                db.DetailTypes.Add(detailtype);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, detailtype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = detailtype.DetailTypeId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/DetailType/5
        public HttpResponseMessage DeleteDetailType(int id)
        {
            DetailType detailtype = db.DetailTypes.Find(id);
            if (detailtype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.DetailTypes.Remove(detailtype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, detailtype);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}