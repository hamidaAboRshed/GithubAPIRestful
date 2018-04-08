using GithubWebApp.Models;
using Newtonsoft.Json;
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
            //List<IEnumerable<GithubUserModel>> userList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Users").Result;
            var msg = response.Content.ReadAsStringAsync().Result;
            //userList = response.Content.ReadAsAsync<List<IEnumerable<GithubUserModel>>>().Result;
            GithubUserModel[] users = JsonConvert.DeserializeObject<GithubUserModel[]>(msg);
            return View(users.ToList());
        }
    }
}