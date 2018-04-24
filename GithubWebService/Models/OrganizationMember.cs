using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace GithubWebService.Models
{
    [Serializable]
    [XmlRoot(DataType = "string", ElementName = "OrganizationMember")]
    [XmlType("OrganizationMember")]
    public class OrganizationMember
    {
        [XmlElement(ElementName = "ID", DataType = "int")]
        public int ID { set; get; }

        //[XmlElement(ElementName = "User", DataType = "User")]
        [XmlIgnore]
        public User User { set; get; }

        [XmlElement(ElementName = "UserID", DataType = "int")]
        public int? UserID { set; get; }

        //[XmlElement(ElementName = "Organization", DataType = "Organization")]
        [XmlIgnore]
        public Organization Organization { set; get; }

        [XmlElement(ElementName = "OrganizationID", DataType = "int")]
        public int? OrganizationID { set; get; }
    }
}