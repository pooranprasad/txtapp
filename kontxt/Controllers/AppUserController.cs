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
    public class AppUserController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/AppUser
        //public IQueryable<AppUser> GetAppUsers()
        //{
        //    return db.AppUsers;
        //}

        // GET api/AppUser/5
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult GetAppUser(string userid, string password)
        {
            AppUser appuser = db.AppUsers.Where(w=>w.UserId==userid && w.Password==password).FirstOrDefault();
            if (appuser == null)
            {
                return NotFound();
            }

            return Ok(appuser);
        }

        [ResponseType(typeof(string))]
        public IHttpActionResult GetAppUser(string userid)
        {
            AppUser appuser = db.AppUsers.Where(w => w.UserId == userid).FirstOrDefault();
            if (appuser == null)
            {
                return NotFound();
            }

            return Ok(appuser.AppUserId);
        }

        // PUT api/AppUser/5
        public IHttpActionResult PutAppUser(Guid id, AppUser appuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appuser.AppUserId)
            {
                return BadRequest();
            }

            db.Entry(appuser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
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

        // POST api/AppUser
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult PostAppUser(AppUser appuser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppUsers.Add(appuser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AppUserExists(appuser.AppUserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = appuser.AppUserId }, appuser);
        }

        // DELETE api/AppUser/5
        [ResponseType(typeof(AppUser))]
        public IHttpActionResult DeleteAppUser(Guid id)
        {
            AppUser appuser = db.AppUsers.Find(id);
            if (appuser == null)
            {
                return NotFound();
            }

            db.AppUsers.Remove(appuser);
            db.SaveChanges();

            return Ok(appuser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppUserExists(Guid id)
        {
            return db.AppUsers.Count(e => e.AppUserId == id) > 0;
        }
    }
}