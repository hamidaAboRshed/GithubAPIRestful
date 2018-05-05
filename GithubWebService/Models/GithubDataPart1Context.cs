﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GithubWebService.Models
{
    public class GithubDataPart1Context : DbContext
    {
        public GithubDataPart1Context()
            : base("GithubServicePart1Connection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
        }
        public static GithubDataPart1Context Create()
        {

            return new GithubDataPart1Context();
        }
    }
}