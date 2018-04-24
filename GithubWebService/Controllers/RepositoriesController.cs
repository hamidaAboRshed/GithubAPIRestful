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
        private static GithubDataContext db = new GithubDataContext();

        //old one
        private static void ReceiveMessageFromQueue(string queueName)
        {
            MessageQueue msMq = msMq = new MessageQueue(queueName);
            try
            {

                 msMq.Formatter = new XmlMessageFormatter(new Type[] {typeof(string)});

                //msMq.Formatter = new XmlMessageFormatter(new Type[] { typeof(Person) });

                var message = msMq.Receive().Body;

                //Console.WriteLine("FirstName: " + message.FirstName + ", LastName: " + message.LastName);

                // Console.WriteLine(message.Body.ToString());

            }

            catch (MessageQueueException ee)
            {

                Console.Write(ee.ToString());

            }

            catch (Exception eee)
            {

                Console.Write(eee.ToString());

            }

            finally
            {

                msMq.Close();

            }

            Console.WriteLine("Message received ......");

        }

        //Get Repository for User
        public static List<Repository> GetRepository(string username)
        {
            User user = db.User.Where(x => x.
                Username == username).FirstOrDefault<User>();
            if (user == null)
            {
                return null;
            }
            return db.Repository.Where(x => x.UserId == user.ID).ToList();
        }


        #region All default method
        // GET: Repositories
        public ActionResult Index()
        {
            var repository = db.Repository.Include(r => r.RepUser);
            return View(repository.ToList());
        }

        // GET: Repositories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repository repository = db.Repository.Find(id);
            if (repository == null)
            {
                return HttpNotFound();
            }
            return View(repository);
        }

        // GET: Repositories/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.User, "ID", "Username");
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
                db.Repository.Add(repository);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.User, "ID", "Username", repository.UserId);
            return View(repository);
        }

        // GET: Repositories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repository repository = db.Repository.Find(id);
            if (repository == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.User, "ID", "Username", repository.UserId);
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
                db.Entry(repository).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.User, "ID", "Username", repository.UserId);
            return View(repository);
        }

        // GET: Repositories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Repository repository = db.Repository.Find(id);
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
            Repository repository = db.Repository.Find(id);
            db.Repository.Remove(repository);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
