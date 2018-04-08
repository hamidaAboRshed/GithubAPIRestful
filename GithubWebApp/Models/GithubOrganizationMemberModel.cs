using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GithubWebApp.Models
{
    public class GithubOrganizationMemberModel
    {
        public int ID { set; get; }
        public GithubUserModel User { set; get; }
        public int? UserID { set; get; }
        public GithubOrganizationModel Organization { set; get; }
        public int? OrganizationID { set; get; }
    }
}