using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GithubWebApi.Models
{
    public class User
    {
        public User()
        {
            RepositoryList = new List<Repository>();
            OrganizationList = new List<Organization>();
        }
        public int ID { set; get; }
        public string Username { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public List<Repository> RepositoryList { set; get; }
        public List<Organization> OrganizationList { set; get; }
    }
}