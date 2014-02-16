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
    public class PhoneNumberController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/PhoneNumber
        public IEnumerable<PhoneNumber> GetPhoneNumbers()
        {
            var phonenumbers = db.PhoneNumbers.Include(p => p.Country);
            return phonenumbers.AsEnumerable();
        }

        // GET api/PhoneNumber/5
        public PhoneNumber GetPhoneNumber(Guid id)
        {
            PhoneNumber phonenumber = db.PhoneNumbers.Find(id);
            if (phonenumber == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return phonenumber;
        }

        // PUT api/PhoneNumber/5
        public HttpResponseMessage PutPhoneNumber(Guid id, PhoneNumber phonenumber)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != phonenumber.PhoneNumberId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(phonenumber).State = EntityState.Modified;

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

        // POST api/PhoneNumber
        public HttpResponseMessage PostPhoneNumber(PhoneNumber phonenumber)
        {
            if (ModelState.IsValid)
            {
                db.PhoneNumbers.Add(phonenumber);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, phonenumber);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = phonenumber.PhoneNumberId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/PhoneNumber/5
        public HttpResponseMessage DeletePhoneNumber(Guid id)
        {
            PhoneNumber phonenumber = db.PhoneNumbers.Find(id);
            if (phonenumber == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.PhoneNumbers.Remove(phonenumber);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, phonenumber);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}