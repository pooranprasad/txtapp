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
    public class ContactPhoneNumberController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/ContactPhoneNumber
        public IEnumerable<ContactPhoneNumber> GetContactPhoneNumbers()
        {
            var contactphonenumbers = db.ContactPhoneNumbers.Include(c => c.Contact).Include(c => c.PhoneNumber);
            return contactphonenumbers.AsEnumerable();
        }

        // GET api/ContactPhoneNumber/5
        public ContactPhoneNumber GetContactPhoneNumber(Guid id)
        {
            ContactPhoneNumber contactphonenumber = db.ContactPhoneNumbers.Find(id);
            if (contactphonenumber == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return contactphonenumber;
        }

        // PUT api/ContactPhoneNumber/5
        public HttpResponseMessage PutContactPhoneNumber(Guid id, ContactPhoneNumber contactphonenumber)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != contactphonenumber.ContactPhoneNumberId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(contactphonenumber).State = EntityState.Modified;

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

        // POST api/ContactPhoneNumber
        public HttpResponseMessage PostContactPhoneNumber(ContactPhoneNumber contactphonenumber)
        {
            if (ModelState.IsValid)
            {
                db.ContactPhoneNumbers.Add(contactphonenumber);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, contactphonenumber);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = contactphonenumber.ContactPhoneNumberId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/ContactPhoneNumber/5
        public HttpResponseMessage DeleteContactPhoneNumber(Guid id)
        {
            ContactPhoneNumber contactphonenumber = db.ContactPhoneNumbers.Find(id);
            if (contactphonenumber == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.ContactPhoneNumbers.Remove(contactphonenumber);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, contactphonenumber);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}