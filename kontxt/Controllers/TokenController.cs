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
    public class TokenController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/Token
        public IEnumerable<Token> GetTokens()
        {
            var tokens = db.Tokens.Include(t => t.AppUser);
            return tokens.AsEnumerable();
        }

        // GET api/Token/5
        public Token GetToken(Guid id)
        {
            Token token = db.Tokens.Find(id);
            if (token == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return token;
        }

        // PUT api/Token/5
        public HttpResponseMessage PutToken(Guid id, Token token)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != token.TokenId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(token).State = EntityState.Modified;

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

        // POST api/Token
        public HttpResponseMessage PostToken(Token token)
        {
            if (ModelState.IsValid)
            {
                db.Tokens.Add(token);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, token);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = token.TokenId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Token/5
        public HttpResponseMessage DeleteToken(Guid id)
        {
            Token token = db.Tokens.Find(id);
            if (token == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Tokens.Remove(token);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, token);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}