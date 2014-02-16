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
    public class EmailAddressController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/EmailAddress
        public IEnumerable<EmailAddress> GetEmailAddresses()
        {
            return db.EmailAddresses.AsEnumerable();
        }

        // GET api/EmailAddress/5
        public EmailAddress GetEmailAddress(Guid id)
        {
            EmailAddress emailaddress = db.EmailAddresses.Find(id);
            if (emailaddress == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return emailaddress;
        }

        // PUT api/EmailAddress/5
        public HttpResponseMessage PutEmailAddress(Guid id, EmailAddress emailaddress)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != emailaddress.EmailAddressId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(emailaddress).State = EntityState.Modified;

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

        // POST api/EmailAddress
        public HttpResponseMessage PostEmailAddress(EmailAddress emailaddress)
        {
            if (ModelState.IsValid)
            {
                db.EmailAddresses.Add(emailaddress);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, emailaddress);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = emailaddress.EmailAddressId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/EmailAddress/5
        public HttpResponseMessage DeleteEmailAddress(Guid id)
        {
            EmailAddress emailaddress = db.EmailAddresses.Find(id);
            if (emailaddress == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.EmailAddresses.Remove(emailaddress);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, emailaddress);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}