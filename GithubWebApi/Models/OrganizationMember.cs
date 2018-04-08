using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GithubWebApi.Models
{
    public class OrganizationMember
    {
        public int ID { set; get; }
        public User User { set; get; }
        public int? UserID { set; get; }
        public Organization Organization { set; get; }
        public int? OrganizationID { set; get; }
    }
}