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
using GithubWebApi.Models;

namespace GithubWebApi.Controllers
{
    public class OrganizationMembersController : ApiController
    {
        private GithubDataContext db = new GithubDataContext();

        // GET: api/OrganizationMembers
        public IQueryable<OrganizationMember> GetOrganizationMember()
        {
            return db.OrganizationMember;
        }

        // GET: api/OrganizationMembers/5
        [ResponseType(typeof(OrganizationMember))]
        public IHttpActionResult GetOrganizationMember(int id)
        {
            OrganizationMember organizationMember = db.OrganizationMember.Find(id);
            if (organizationMember == null)
            {
                return NotFound();
            }

            return Ok(organizationMember);
        }

        // PUT: api/OrganizationMembers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrganizationMember(int id, OrganizationMember organizationMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organizationMember.ID)
            {
                return BadRequest();
            }

            db.Entry(organizationMember).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationMemberExists(id))
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

        // POST: api/OrganizationMembers
        [ResponseType(typeof(OrganizationMember))]
        public IHttpActionResult PostOrganizationMember(OrganizationMember organizationMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrganizationMember.Add(organizationMember);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = organizationMember.ID }, organizationMember);
        }

        // DELETE: api/OrganizationMembers/5
        [ResponseType(typeof(OrganizationMember))]
        public IHttpActionResult DeleteOrganizationMember(int id)
        {
            OrganizationMember organizationMember = db.OrganizationMember.Find(id);
            if (organizationMember == null)
            {
                return NotFound();
            }

            db.OrganizationMember.Remove(organizationMember);
            db.SaveChanges();

            return Ok(organizationMember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationMemberExists(int id)
        {
            return db.OrganizationMember.Count(e => e.ID == id) > 0;
        }
    }
}