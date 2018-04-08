using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GithubWebApi.Models
{
    public class Organization
    {
        public Organization()
        {
            OrganizationMemberList = new List<OrganizationMember>();
        }
        public int ID { set; get; }
        public string Name { set; get; }
        public string BillingEmail { set; get; }

        public List<OrganizationMember> OrganizationMemberList { set; get; }
    }
}