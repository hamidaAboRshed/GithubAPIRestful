namespace GithubWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Organizations", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Organizations", "Name", c => c.String());
        }
    }
}
