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
    public class AppUserContactController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/AppUserContact
        public IEnumerable<AppUserContact> GetAppUserContacts()
        {
            var appusercontacts = db.AppUserContacts.Include(a => a.AppUser).Include(a => a.Contact);
            return appusercontacts.AsEnumerable();
        }

        // GET api/AppUserContact/5
        public AppUserContact GetAppUserContact(Guid id)
        {
            AppUserContact appusercontact = db.AppUserContacts.Find(id);
            if (appusercontact == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return appusercontact;
        }

        // PUT api/AppUserContact/5
        public HttpResponseMessage PutAppUserContact(Guid id, AppUserContact appusercontact)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != appusercontact.AppUserContactId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(appusercontact).State = EntityState.Modified;

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

        // POST api/AppUserContact
        public HttpResponseMessage PostAppUserContact(AppUserContact appusercontact)
        {
            if (ModelState.IsValid)
            {
                db.AppUserContacts.Add(appusercontact);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, appusercontact);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = appusercontact.AppUserContactId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/AppUserContact/5
        public HttpResponseMessage DeleteAppUserContact(Guid id)
        {
            AppUserContact appusercontact = db.AppUserContacts.Find(id);
            if (appusercontact == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.AppUserContacts.Remove(appusercontact);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, appusercontact);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}