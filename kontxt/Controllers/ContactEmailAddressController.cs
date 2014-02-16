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
    public class ContactEmailAddressController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/ContactEmailAddress
        public IEnumerable<ContactEmailAddress> GetContactEmailAddresses()
        {
            var contactemailaddresses = db.ContactEmailAddresses.Include(c => c.Contact).Include(c => c.EmailAddress);
            return contactemailaddresses.AsEnumerable();
        }

        // GET api/ContactEmailAddress/5
        public ContactEmailAddress GetContactEmailAddress(Guid id)
        {
            ContactEmailAddress contactemailaddress = db.ContactEmailAddresses.Find(id);
            if (contactemailaddress == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return contactemailaddress;
        }

        // PUT api/ContactEmailAddress/5
        public HttpResponseMessage PutContactEmailAddress(Guid id, ContactEmailAddress contactemailaddress)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != contactemailaddress.ContactEmailAddressId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(contactemailaddress).State = EntityState.Modified;

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

        // POST api/ContactEmailAddress
        public HttpResponseMessage PostContactEmailAddress(ContactEmailAddress contactemailaddress)
        {
            if (ModelState.IsValid)
            {
                db.ContactEmailAddresses.Add(contactemailaddress);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, contactemailaddress);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = contactemailaddress.ContactEmailAddressId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ContactEmailAddress/5
        public HttpResponseMessage DeleteContactEmailAddress(Guid id)
        {
            ContactEmailAddress contactemailaddress = db.ContactEmailAddresses.Find(id);
            if (contactemailaddress == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ContactEmailAddresses.Remove(contactemailaddress);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, contactemailaddress);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}