﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using ProjectH.Models;

namespace ProjectH.Controllers
{
    public class StaffsController : ApiController
    {
        private HostelsEntities db = new HostelsEntities();
        [Authorize]
        // GET: api/Staffs
        public IQueryable<Staff> GetStaffs()
        {
            return db.Staffs;
        }

        // GET: api/Staffs/5
        [Authorize]
        [ResponseType(typeof(Staff))]
        public IHttpActionResult GetStaff(int id)
        {
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }


        // PUT: api/Staffs/5
        [Authorize(Roles = "SuperAdmin")]
        [ResponseType(typeof(void))]

        public IHttpActionResult PutStaff(int id, Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != staff.Staffid)
            {
                return BadRequest();
            }

            db.Entry(staff).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
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

        // POST: api/Staffs
        [Authorize(Roles = "SuperAdmin")]
        [ResponseType(typeof(Staff))]
        public IHttpActionResult PostStaff(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Staffs.Add(staff);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StaffExists(staff.Staffid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = staff.Staffid }, staff);
        }

        // DELETE: api/Staffs/5
        [Authorize(Roles = "SuperAdmin")]
        [ResponseType(typeof(Staff))]
        public IHttpActionResult DeleteStaff(int id)
        {
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return NotFound();
            }

            db.Staffs.Remove(staff);
            db.SaveChanges();

            return Ok(staff);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StaffExists(int id)
        {
            return db.Staffs.Count(e => e.Staffid == id) > 0;
        }
    }
}