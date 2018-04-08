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
    builder.EntitySet<Repository>("Repositories");
    builder.EntitySet<User>("User"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RepositoriesController : ODataController
    {
        private GithubDataContext db = new GithubDataContext();

        // GET: odata/Repositories
        [EnableQuery]
        public IQueryable<Repository> GetRepositories()
        {
            return db.Repository;
        }

        // GET: odata/Repositories(5)
        [EnableQuery]
        public SingleResult<Repository> GetRepository([FromODataUri] int key)
        {
            return SingleResult.Create(db.Repository.Where(repository => repository.ID == key));
        }

        // PUT: odata/Repositories(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Repository> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Repository repository = db.Repository.Find(key);
            if (repository == null)
            {
                return NotFound();
            }

            patch.Put(repository);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepositoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(repository);
        }

        // POST: odata/Repositories
        public IHttpActionResult Post(Repository repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Repository.Add(repository);
            db.SaveChanges();

            return Created(repository);
        }

        // PATCH: odata/Repositories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Repository> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Repository repository = db.Repository.Find(key);
            if (repository == null)
            {
                return NotFound();
            }

            patch.Patch(repository);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepositoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(repository);
        }

        // DELETE: odata/Repositories(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Repository repository = db.Repository.Find(key);
            if (repository == null)
            {
                return NotFound();
            }

            db.Repository.Remove(repository);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Repositories(5)/RepUser
        [EnableQuery]
        public SingleResult<User> GetRepUser([FromODataUri] int key)
        {
            return SingleResult.Create(db.Repository.Where(m => m.ID == key).Select(m => m.RepUser));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RepositoryExists(int key)
        {
            return db.Repository.Count(e => e.ID == key) > 0;
        }
    }
}
