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
    builder.EntitySet<Organization>("Organizations");
    builder.EntitySet<OrganizationMember>("OrganizationMember"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class OrganizationsController : ODataController
    {
        private GithubDataContext db = new GithubDataContext();

        // GET: odata/Organizations
        [EnableQuery]
        public IQueryable<Organization> GetOrganizations()
        {
            return db.Organization;
        }

        // GET: odata/Organizations(5)
        [EnableQuery]
        public SingleResult<Organization> GetOrganization([FromODataUri] int key)
        {
            return SingleResult.Create(db.Organization.Where(organization => organization.ID == key));
        }

        // PUT: odata/Organizations(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Organization> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Organization organization = db.Organization.Find(key);
            if (organization == null)
            {
                return NotFound();
            }

            patch.Put(organization);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organization);
        }

        // POST: odata/Organizations
        public IHttpActionResult Post(Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Organization.Add(organization);
            db.SaveChanges();

            return Created(organization);
        }

        // PATCH: odata/Organizations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Organization> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Organization organization = db.Organization.Find(key);
            if (organization == null)
            {
                return NotFound();
            }

            patch.Patch(organization);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(organization);
        }

        // DELETE: odata/Organizations(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Organization organization = db.Organization.Find(key);
            if (organization == null)
            {
                return NotFound();
            }

            db.Organization.Remove(organization);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Organizations(5)/OrganizationMemberList
        [EnableQuery]
        public IQueryable<OrganizationMember> GetOrganizationMemberList([FromODataUri] int key)
        {
            return db.Organization.Where(m => m.ID == key).SelectMany(m => m.OrganizationMemberList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrganizationExists(int key)
        {
            return db.Organization.Count(e => e.ID == key) > 0;
        }
    }
}
