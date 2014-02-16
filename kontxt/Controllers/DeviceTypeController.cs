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
    public class DeviceTypeController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/DeviceType
        public IQueryable<DeviceType> GetDeviceTypes()
        {
            return db.DeviceTypes;
        }

        // GET api/DeviceType/5
        [ResponseType(typeof(DeviceType))]
        public IHttpActionResult GetDeviceType(int id)
        {
            DeviceType devicetype = db.DeviceTypes.Find(id);
            if (devicetype == null)
            {
                return NotFound();
            }

            return Ok(devicetype);
        }

        // PUT api/DeviceType/5
        public IHttpActionResult PutDeviceType(int id, DeviceType devicetype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != devicetype.DeviceTypeId)
            {
                return BadRequest();
            }

            db.Entry(devicetype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceTypeExists(id))
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

        // POST api/DeviceType
        [ResponseType(typeof(DeviceType))]
        public IHttpActionResult PostDeviceType(DeviceType devicetype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeviceTypes.Add(devicetype);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = devicetype.DeviceTypeId }, devicetype);
        }

        // DELETE api/DeviceType/5
        [ResponseType(typeof(DeviceType))]
        public IHttpActionResult DeleteDeviceType(int id)
        {
            DeviceType devicetype = db.DeviceTypes.Find(id);
            if (devicetype == null)
            {
                return NotFound();
            }

            db.DeviceTypes.Remove(devicetype);
            db.SaveChanges();

            return Ok(devicetype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeviceTypeExists(int id)
        {
            return db.DeviceTypes.Count(e => e.DeviceTypeId == id) > 0;
        }
    }
}