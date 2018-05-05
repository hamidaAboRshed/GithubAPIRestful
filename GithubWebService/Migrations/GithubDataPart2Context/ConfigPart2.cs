namespace GithubWebService.Migrations.GithubDataPart2Context
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ConfigPart2 : DbMigrationsConfiguration<GithubWebService.Models.GithubDataPart2Context>
    {
        public ConfigPart2()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\GithubDataPart2Context";
        }

        protected override void Seed(GithubWebService.Models.GithubDataPart2Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
