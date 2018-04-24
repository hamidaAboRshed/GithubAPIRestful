﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace GithubWebService.Models
{
    [Serializable]
    [XmlRoot(DataType = "string", ElementName = "Repository")]
    [XmlType("Repository")]
    public class Repository
    {
        public Repository()
        {

        }

        public Repository(int id, string name, string desc, string lang, User user, int userID)
        {
            ID = id;
            Name = name;
            Description = desc;
            Language = lang;
            RepUser = user;
            UserId=userID;
        }
        [XmlElement(ElementName = "ID", DataType = "int")]
        public int ID { set; get; }

        [XmlElement(ElementName = "Name", DataType = "string")]
        [Required]
        public string Name { set; get; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        [XmlElement(ElementName = "Description", DataType = "string")]
        public string Description { set; get; }

        [XmlElement(ElementName = "Language", DataType = "string")]
        public string Language { set; get; }

        [XmlIgnore]
        public User RepUser { set; get; }

        [XmlElement(ElementName = "UserId", DataType = "int")]
        public int? UserId { set; get; }
    }
}