using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using kontxt.Models;

namespace kontxt.Controllers
{
    public class TokenController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/Token
        public IQueryable<Token> GetTokens()
        {
            return db.Tokens;
        }

        // GET api/Token/5
        [ResponseType(typeof(Token))]
        public IHttpActionResult GetToken(Guid id)
        {
            Token token = db.Tokens.Find(id);
            if (token == null)
            {
                return NotFound();
            }

            return Ok(token);
        }

        // PUT api/Token/5
        public IHttpActionResult PutToken(Guid id, Token token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != token.TokenId)
            {
                return BadRequest();
            }

            db.Entry(token).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TokenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Token
        [ResponseType(typeof(Token))]
        public IHttpActionResult PostToken(Token token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tokens.Add(token);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TokenExists(token.TokenId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = token.TokenId }, token);
        }

        // DELETE api/Token/5
        [ResponseType(typeof(Token))]
        public IHttpActionResult DeleteToken(Guid id)
        {
            Token token = db.Tokens.Find(id);
            if (token == null)
            {
                return NotFound();
            }

            db.Tokens.Remove(token);
            db.SaveChanges();

            return Ok(token);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TokenExists(Guid id)
        {
            return db.Tokens.Count(e => e.TokenId == id) > 0;
        }
    }
}