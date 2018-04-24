namespace GithubWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hamida1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organizations", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Repositories", "Description", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Repositories", "Description", c => c.String());
            AlterColumn("dbo.Organizations", "Name", c => c.String(nullable: false));
        }
    }
}
