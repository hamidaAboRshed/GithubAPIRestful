using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GithubWebApp.Models
{
    public class GithubRepositoryModel
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Language { set; get; }
        public GithubUserModel RepUser { set; get; }
        public int? UserId { set; get; }
    }
}