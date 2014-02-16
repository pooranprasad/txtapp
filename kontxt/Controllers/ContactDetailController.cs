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
    public class ContactDetailController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/ContactDetail
        public IEnumerable<ContactDetail> GetContactDetails()
        {
            var contactdetails = db.ContactDetails.Include(c => c.Contact).Include(c => c.Detail);
            return contactdetails.AsEnumerable();
        }

        // GET api/ContactDetail/5
        public ContactDetail GetContactDetail(Guid id)
        {
            ContactDetail contactdetail = db.ContactDetails.Find(id);
            if (contactdetail == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return contactdetail;
        }

        // PUT api/ContactDetail/5
        public HttpResponseMessage PutContactDetail(Guid id, ContactDetail contactdetail)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != contactdetail.ContactDetailId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(contactdetail).State = EntityState.Modified;

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

        // POST api/ContactDetail
        public HttpResponseMessage PostContactDetail(ContactDetail contactdetail)
        {
            if (ModelState.IsValid)
            {
                db.ContactDetails.Add(contactdetail);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, contactdetail);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = contactdetail.ContactDetailId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ContactDetail/5
        public HttpResponseMessage DeleteContactDetail(Guid id)
        {
            ContactDetail contactdetail = db.ContactDetails.Find(id);
            if (contactdetail == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ContactDetails.Remove(contactdetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, contactdetail);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}