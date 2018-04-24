using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace GithubWebService.Models
{
    [Serializable]
    [XmlRoot(DataType = "string", ElementName = "User")]
    [XmlType("User")]
    public class User
    {
        public User()
        {
            RepositoryList = new List<Repository>();
            OrganizationList = new List<Organization>();
        }
        [XmlElement(ElementName = "ID", DataType = "int")]
        public int ID { set; get; }

        [XmlElement(ElementName = "Username", DataType = "string")]
        public string Username { set; get; }

        [XmlElement(ElementName = "Email", DataType = "string")]
        public string Email { set; get; }

        [XmlElement(ElementName = "Password", DataType = "string")]
        public string Password { set; get; }

        //[XmlElement(ElementName = "RepositoryList", DataType = "Repository")]
        [XmlIgnore]
        public List<Repository> RepositoryList { set; get; }

        //[XmlElement(ElementName = "OrganizationList", DataType = "Organization")]
        [XmlIgnore]
        public List<Organization> OrganizationList { set; get; }
    }
}