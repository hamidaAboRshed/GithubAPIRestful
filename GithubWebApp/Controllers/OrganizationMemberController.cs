using GithubWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace GithubWebApp.Controllers
{
    public class OrganizationMemberController : Controller
    {
        // GET: OrganizationMember
        public ActionResult Index()
        {
            IEnumerable<GithubOrganizationMemberModel> organizationMemberList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("OrganizationMembers").Result;
            organizationMemberList = response.Content.ReadAsAsync<IEnumerable<GithubOrganizationMemberModel>>().Result;
            return View(organizationMemberList);
        }
    }
}