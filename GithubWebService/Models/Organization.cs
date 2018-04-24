using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace GithubWebService.Models
{
    //[Serializable]
    [XmlRoot(DataType = "string", ElementName = "Organization")]
    public class Organization
    {
        public Organization()
        {
            OrganizationMemberList = new List<OrganizationMember>();
        }

        [Required]
        [XmlElement(ElementName = "ID", DataType = "int")]
        public int ID { set; get; }
       
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        [XmlElement(ElementName = "Name", DataType = "string")]
        public string Name { set; get; }

        [Required]
        [EmailAddress]
        [XmlElement(ElementName = "BillingEmail", DataType = "string")]
        public string BillingEmail { set; get; }

        //[XmlElement(ElementName= "OrganizationMemberList",DataType="string")]
        [XmlIgnore]
        public List<OrganizationMember> OrganizationMemberList { set; get; }
    }
}