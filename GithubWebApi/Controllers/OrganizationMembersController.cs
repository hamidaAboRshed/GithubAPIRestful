using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using GithubWebApi.Models;

namespace GithubWebApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using GithubWebApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<OrganizationMember>("OrganizationMembers");
    builder.EntitySet<Organization>("Organization"); 
    builder.EntitySet<User>("User"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OrganizationMembersController : ODataController
    {
        private GithubDataContext db = new GithubDataContext();

        // GET: odata/OrganizationMembers
        [EnableQuery]
        public IQueryable<OrganizationMember> GetOrganizationMembers()
        {
            return db.OrganizationMember;
        }

        // GET: odata/OrganizationMembers(5)
        [EnableQuery]
        public SingleResult<OrganizationMember> GetOrganizationMember([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrganizationMember.Where(organizationMember => organizationMember.ID == key));
        }

        // PUT: odata/OrganizationMembers(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<OrganizationMember> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrganizationMember organizationMember = db.OrganizationMember.Find(key);
            if (organizationMember == null)
            {
                return NotFound();
            }

            patch.Put(organizationMember);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationMemberExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organizationMember);
        }

        // POST: odata/OrganizationMembers
        public IHttpActionResult Post(OrganizationMember organizationMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrganizationMember.Add(organizationMember);
            db.SaveChanges();

            return Created(organizationMember);
        }

        // PATCH: odata/OrganizationMembers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<OrganizationMember> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            OrganizationMember organizationMember = db.OrganizationMember.Find(key);
            if (organizationMember == null)
            {
                return NotFound();
            }

            patch.Patch(organizationMember);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationMemberExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organizationMember);
        }

        // DELETE: odata/OrganizationMembers(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            OrganizationMember organizationMember = db.OrganizationMember.Find(key);
            if (organizationMember == null)
            {
                return NotFound();
            }

            db.OrganizationMember.Remove(organizationMember);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/OrganizationMembers(5)/Organization
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrganizationMember.Where(m => m.ID == key).Select(m => m.Organization));
        }

        // GET: odata/OrganizationMembers(5)/User
        [EnableQuery]
        public SingleResult<User> GetUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.OrganizationMember.Where(m => m.ID == key).Select(m => m.User));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationMemberExists(int key)
        {
            return db.OrganizationMember.Count(e => e.ID == key) > 0;
        }
    }
}
