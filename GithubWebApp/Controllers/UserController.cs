using GithubWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GithubWebApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            IEnumerable<GithubUserModel> userList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Users").Result;
            userList = response.Content.ReadAsAsync<IEnumerable<GithubUserModel>>().Result;
            return View(userList);
        }
    }
}