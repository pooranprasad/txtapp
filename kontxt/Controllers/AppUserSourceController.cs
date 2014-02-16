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
    public class AppUserSourceController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/AppUserSource
        public IEnumerable<AppUserSource> GetAppUserSources()
        {
            var appusersources = db.AppUserSources.Include(a => a.AppUser).Include(a => a.Source);
            return appusersources.AsEnumerable();
        }

        // GET api/AppUserSource/5
        public AppUserSource GetAppUserSource(Guid id)
        {
            AppUserSource appusersource = db.AppUserSources.Find(id);
            if (appusersource == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return appusersource;
        }

        // PUT api/AppUserSource/5
        public HttpResponseMessage PutAppUserSource(Guid id, AppUserSource appusersource)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != appusersource.AppUserSourceId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(appusersource).State = EntityState.Modified;

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

        // POST api/AppUserSource
        public HttpResponseMessage PostAppUserSource(AppUserSource appusersource)
        {
            if (ModelState.IsValid)
            {
                db.AppUserSources.Add(appusersource);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, appusersource);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = appusersource.AppUserSourceId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/AppUserSource/5
        public HttpResponseMessage DeleteAppUserSource(Guid id)
        {
            AppUserSource appusersource = db.AppUserSources.Find(id);
            if (appusersource == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.AppUserSources.Remove(appusersource);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, appusersource);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}