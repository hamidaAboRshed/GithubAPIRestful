using GithubWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GithubWebApp.Controllers
{
    public class OrganizationController : Controller
    {
        // GET: Organization
        public ActionResult Index()
        {
            IEnumerable<GithubOrganizationModel> organizationMemberList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Organizations").Result;
            organizationMemberList = response.Content.ReadAsAsync<IEnumerable<GithubOrganizationModel>>().Result;
            return View(organizationMemberList);
        }

        public ActionResult AddorEdit(int id = 0)
        {
            if (id == 0)
                return View(new GithubOrganizationModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Organizations/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<GithubOrganizationModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddorEdit(GithubOrganizationModel Org)
        {
            if (Org.ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Organizations", Org).Result;
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Organizations/" + Org.ID, Org).Result;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Organizations/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }

    }
}