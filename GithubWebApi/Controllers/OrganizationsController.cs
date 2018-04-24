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
    public class OrganizationsController : ApiController
    {
        private GithubDataContext db = new GithubDataContext();

        // GET: api/Organizations
        public IQueryable<Organization> GetOrganization()
        {
            return db.Organization;
        }

        // GET: api/Organizations/5
        [ResponseType(typeof(Organization))]
        public IHttpActionResult GetOrganization(int id)
        {
            Organization organization = db.Organization.Find(id);
            if (organization == null)
            {
                return NotFound();
            }

            return Ok(organization);
        }

        [HttpPost]
        [Route("save")]
        public HttpResponseMessage save(Organization org)
        {
            try
            {
                if (ModelState.IsValid)
                    return new HttpResponseMessage(HttpStatusCode.OK);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ModelState);
            }

        }

        // PUT: api/Organizations/5
        [ResponseType(typeof(void))]
        public HttpResponseMessage PutOrganization(int id, Organization organization)
        {
            if(id != organization.ID)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if(ModelState.IsValid)
            {
                db.Entry(organization).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!OrganizationExists(id))
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid ID");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
           /* if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != organization.ID)
            {
                return BadRequest();
            }

            db.Entry(organization).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);*/
        }

        // POST: api/Organizations
        [ResponseType(typeof(Organization))]
        public HttpResponseMessage PostOrganization(Organization organization)
        {
           /* if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Organization.Add(organization);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = organization.ID }, organization);*/
            try
            {
                if(ModelState.IsValid)
                {
                    db.Organization.Add(organization);
                    db.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE: api/Organizations/5
        [ResponseType(typeof(Organization))]
        public IHttpActionResult DeleteOrganization(int id)
        {
            Organization organization = db.Organization.Find(id);
            if (organization == null)
            {
                return NotFound();
            }

            db.Organization.Remove(organization);
            db.SaveChanges();

            return Ok(organization);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationExists(int id)
        {
            return db.Organization.Count(e => e.ID == id) > 0;
        }
    }
}