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
    public class DeviceTypeController : ApiController
    {
        private kontxtEntities db = new kontxtEntities();

        // GET api/DeviceType
        public IEnumerable<DeviceType> GetDeviceTypes()
        {
            return db.DeviceTypes.AsEnumerable();
        }

        // GET api/DeviceType/5
        public DeviceType GetDeviceType(int id)
        {
            DeviceType devicetype = db.DeviceTypes.Find(id);
            if (devicetype == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return devicetype;
        }

        // PUT api/DeviceType/5
        public HttpResponseMessage PutDeviceType(int id, DeviceType devicetype)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != devicetype.DeviceTypeId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(devicetype).State = EntityState.Modified;

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

        // POST api/DeviceType
        public HttpResponseMessage PostDeviceType(DeviceType devicetype)
        {
            if (ModelState.IsValid)
            {
                db.DeviceTypes.Add(devicetype);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, devicetype);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = devicetype.DeviceTypeId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/DeviceType/5
        public HttpResponseMessage DeleteDeviceType(int id)
        {
            DeviceType devicetype = db.DeviceTypes.Find(id);
            if (devicetype == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.DeviceTypes.Remove(devicetype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, devicetype);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}