using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public int ID { set; get; }
       
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { set; get; }

        [Requirerd]
        [EmailAddress]
        public string BillingEmail { set; get; }

        public List<OrganizationMember> OrganizationMemberList { set; get; }
    }
}