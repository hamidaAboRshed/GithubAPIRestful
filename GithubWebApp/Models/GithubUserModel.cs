using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GithubWebApp.Models
{
    public class GithubUserModel
    {
        public GithubUserModel()
        {
            RepositoryList = new List<GithubRepositoryModel>();
            OrganizationList = new List<GithubOrganizationModel>();
        }
        public int ID { set; get; }
        public string Username { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public List<GithubRepositoryModel> RepositoryList { set; get; }
        public List<GithubOrganizationModel> OrganizationList { set; get; }
    }
}