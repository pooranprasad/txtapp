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
    public class ContactLinkController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/ContactLink
        public IEnumerable<ContactLink> GetContactLinks()
        {
            var contactlinks = db.ContactLinks.Include(c => c.Contact).Include(c => c.Contact1);
            return contactlinks.AsEnumerable();
        }

        // GET api/ContactLink/5
        public ContactLink GetContactLink(Guid id)
        {
            ContactLink contactlink = db.ContactLinks.Find(id);
            if (contactlink == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return contactlink;
        }

        // PUT api/ContactLink/5
        public HttpResponseMessage PutContactLink(Guid id, ContactLink contactlink)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != contactlink.ContactLinkId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(contactlink).State = EntityState.Modified;

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

        // POST api/ContactLink
        public HttpResponseMessage PostContactLink(ContactLink contactlink)
        {
            if (ModelState.IsValid)
            {
                db.ContactLinks.Add(contactlink);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, contactlink);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = contactlink.ContactLinkId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ContactLink/5
        public HttpResponseMessage DeleteContactLink(Guid id)
        {
            ContactLink contactlink = db.ContactLinks.Find(id);
            if (contactlink == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ContactLinks.Remove(contactlink);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, contactlink);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}