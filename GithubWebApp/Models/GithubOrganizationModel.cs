using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GithubWebApp.Models
{
    public class GithubOrganizationModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string BillingEmail { set; get; }
        public List<GithubOrganizationModel> OrganizationMemberList { set; get; }
    }
}