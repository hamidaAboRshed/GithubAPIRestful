using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GithubWebService.Models;
using System.Messaging;

namespace GithubWebService.Controllers
{
    public class RepositoriesController : Controller
    {
        private static GithubDataPart1Context db1 = new GithubDataPart1Context();
        private static GithubDataPart2Context db2 = new GithubDataPart2Context();

        //Get Repository for User
        public static List<Repository> GetRepository(string username)
        {
            User user = db1.User.Where(x => x.
                Username == username).FirstOrDefault<User>();
            if (user == null)
            {
                return null;
            }
            return db2.Repository.Where(x => x.UserId == user.ID).ToList();
        }


        #region All default method
        // GET: Repositories
        public ActionResult Index()
        {
            var repository = db2.Repository.Include(r => r.RepUser);
            return View(repository.ToList());
        }

        // GET: Repositories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repository repository = db2.Repository.Find(id);
            if (repository == null)
            {
                return HttpNotFound();
            }
            return View(repository);
        }

        // GET: Repositories/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db1.User, "ID", "Username");
            return View();
        }

        // POST: Repositories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Language,UserId")] Repository repository)
        {
            if (ModelState.IsValid)
            {
                db2.Repository.Add(repository);
                db2.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db1.User, "ID", "Username", repository.UserId);
            return View(repository);
        }

        // GET: Repositories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repository repository = db2.Repository.Find(id);
            if (repository == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db1.User, "ID", "Username", repository.UserId);
            return View(repository);
        }

        // POST: Repositories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Language,UserId")] Repository repository)
        {
            if (ModelState.IsValid)
            {
                db2.Entry(repository).State = EntityState.Modified;
                db2.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db1.User, "ID", "Username", repository.UserId);
            return View(repository);
        }

        // GET: Repositories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repository repository = db2.Repository.Find(id);
            if (repository == null)
            {
                return HttpNotFound();
            }
            return View(repository);
        }

        // POST: Repositories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Repository repository = db2.Repository.Find(id);
            db2.Repository.Remove(repository);
            db2.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db2.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
