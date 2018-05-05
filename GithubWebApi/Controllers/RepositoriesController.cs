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
using System.Messaging;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Linq;

namespace GithubWebApi.Controllers
{
    public class RepositoriesController : ApiController
    {
        private GithubDataContext db = new GithubDataContext();

        // GET: api/Repositories
        //public IQueryable<Repository> GetRepository()
        //{
        //    return db.Repository;
        //}
        
        // GET: api/Repositories/5
        [ResponseType(typeof(Repository))]
        public IHttpActionResult GetRepository(int id)
        {
            Repository repository = db.Repository.Find(id);
            if (repository == null)
            {
                return NotFound();
            }

            return Ok(repository);
        }

        const string queueName = @".\private$\GithubQueue";
        //get all repository for user by username
        [ResponseType(typeof(Repository))]
        [Route("api/users/{username}/repos")]
        // check if queue exists, if not create it
        public string GetRepository(string username)
        {
            MessageQueue msMq = null;

            if (!MessageQueue.Exists(queueName))
            {
                msMq = MessageQueue.Create(queueName);
            }
            else
            {
                msMq = new MessageQueue(queueName);
                
            }

            try
            {
                msMq.Send(username);
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

            //waitMessage wait_message = new waitMessage();
            string wait_message = "Your request is processing .. Please wait";
            return wait_message;
            
        }
        [ResponseType(typeof(Repository))]
        [Route("api/users/repos/get")]
        public List<Repository> GetRepository()
        {
            //listener

            MessageQueue myQueue = new MessageQueue(@".\private$\GithubQueueRepo");
            myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });

            
            // End the asynchronous Receive operation.
            Message m = myQueue.Receive();

            // Display message information on the screen.
            List<Repository> repositoryList = getRepositoryList((string)m.Body);


            return repositoryList;
            
        }

        private static void MyReceiveCompleted(Object source,
           ReceiveCompletedEventArgs asyncResult)
        {
            // Connect to the queue.
            MessageQueue mq = (MessageQueue)source;

            // End the asynchronous Receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            // Display message information on the screen.
            List<Repository> t=getRepositoryList((string)m.Body);

            
            // Restart the asynchronous Receive operation.
            mq.BeginReceive();

            return;
        }
        
        //get data from sevice
        public static List<Repository> getRepositoryList(string msg)
        {
            var serializer = new XmlSerializer(typeof(List<Repository>));
            List<Repository> result;

            using (TextReader reader = new StringReader(msg))
            {
                result = (List<Repository>)serializer.Deserialize(reader);
            }

            return result;
         }
        //public IEnumerable<Repository> GetRepository(string username)
        //{
        //    User user = db.User.Where(x => x.
        //        Username == username).FirstOrDefault<User>();
        //    if (user == null)
        //    {
        //        return null;
        //    }
        //    return db.Repository.Where(x => x.UserId == user.ID).ToList();
        //}

        [HttpPost]
        [Route("save")]
        public HttpResponseMessage save(Repository repository)
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

        // PUT: api/Repositories/5
        [ResponseType(typeof(void))]
        public HttpResponseMessage PutRepository(int id, Repository repository)
        {
            if (id != repository.ID)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (ModelState.IsValid)
            {
                db.Entry(repository).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepositoryExists(id))
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

            if (id != repository.ID)
            {
                return BadRequest();
            }

            db.Entry(repository).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepositoryExists(id))
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

        // POST: api/Repositories
        [ResponseType(typeof(Repository))]
        public IHttpActionResult PostRepository(Repository repository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Repository.Add(repository);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = repository.ID }, repository);
        }

        // DELETE: api/Repositories/5
        [ResponseType(typeof(Repository))]
        public IHttpActionResult DeleteRepository(int id)
        {
            Repository repository = db.Repository.Find(id);
            if (repository == null)
            {
                return NotFound();
            }

            db.Repository.Remove(repository);
            db.SaveChanges();

            return Ok(repository);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RepositoryExists(int id)
        {
            return db.Repository.Count(e => e.ID == id) > 0;
        }
    }
}