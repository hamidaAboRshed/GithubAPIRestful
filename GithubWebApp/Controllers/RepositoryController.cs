using GithubWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GithubWebApp.Controllers
{
    public class RepositoryController : Controller
    {
        static int? userId=0;
        static string _username = "initial";
        // GET: Repository

        //public ActionResult Index()
        //{
        //    IEnumerable<GithubRepositoryModel> repositoriesList;
        //    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Repositories").Result;
        //    repositoriesList = response.Content.ReadAsAsync<IEnumerable<GithubRepositoryModel>>().Result;
        //    return View(repositoriesList);
        //}

        //public ActionResult Index(int id)
        //{
        //    IEnumerable<GithubRepositoryModel> repositoriesList;
        //    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Repositories" + id.ToString()).Result;
        //    repositoriesList = response.Content.ReadAsAsync<IEnumerable<GithubRepositoryModel>>().Result;
        //    return View(repositoriesList);
        //}
        public ActionResult SearchForUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username)
        {
            string msg;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("users/" + username + "/repos".ToString()).Result;
            msg = response.Content.ReadAsAsync<string>().Result;
            ViewData["Message"] = msg;
            return View();
        }
        public ActionResult GetResponse()
        {
            IEnumerable<GithubRepositoryModel> repositoriesList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("users/repos/get".ToString()).Result;
            repositoriesList = response.Content.ReadAsAsync<IEnumerable<GithubRepositoryModel>>().Result;
            return View(repositoriesList);
        }

        //[HttpPost]
        //public ActionResult Index(string username)
        //{
        //    //if(username=="search")
        //    //    _username = Request.Form["username"];
        //    _username = username;
        //    IEnumerable<GithubRepositoryModel> repositoriesList;
        //    HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("users/"+ username+"/repos").Result;
        //    repositoriesList = response.Content.ReadAsAsync<List<GithubRepositoryModel>>().Result;
        //    if (repositoriesList != null)
        //    {
        //        if (repositoriesList.Count() != 0)
        //            userId = repositoriesList.ElementAt(0).UserId;
        //        else
        //        {
        //            GithubUserModel user;
        //            HttpResponseMessage response2 = GlobalVariables.WebApiClient.GetAsync("basic/" + username + "/user".ToString()).Result;
        //            user = response2.Content.ReadAsAsync<GithubUserModel>().Result;
        //            userId = user.ID;
        //        }
        //    }
        //    else
        //    {
        //        userId = 0;
        //        return View("~/Views/Shared/Error.cshtml");
        //    }
        //    //return null;
        //    return View(repositoriesList);
        //}
        [HttpGet]
        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
            {
                //GithubRepositoryModel rep = new GithubRepositoryModel();
                //rep.UserId = userId;
                return View(new GithubRepositoryModel());
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Repositories/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<GithubRepositoryModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddorEdit(GithubRepositoryModel Rep)
        {
            if (Rep.ID == 0)
            {
                Rep.UserId = userId;
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Repositories", Rep).Result;
            }
            else
            {
                Rep.UserId = userId;
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Repositories/" + Rep.ID, Rep).Result;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Repositories/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }

    }
}