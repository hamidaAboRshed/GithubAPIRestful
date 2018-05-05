namespace GithubWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrganizationMembers", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.OrganizationMembers", "UserID", "dbo.Users");
            DropForeignKey("dbo.Repositories", "UserId", "dbo.Users");
            DropIndex("dbo.OrganizationMembers", new[] { "UserID" });
            DropIndex("dbo.OrganizationMembers", new[] { "OrganizationID" });
            DropIndex("dbo.Repositories", new[] { "UserId" });
            AlterColumn("dbo.OrganizationMembers", "UserID", c => c.Int());
            AlterColumn("dbo.OrganizationMembers", "OrganizationID", c => c.Int());
            AlterColumn("dbo.Repositories", "UserId", c => c.Int());
            CreateIndex("dbo.OrganizationMembers", "UserID");
            CreateIndex("dbo.OrganizationMembers", "OrganizationID");
            CreateIndex("dbo.Repositories", "UserId");
            AddForeignKey("dbo.OrganizationMembers", "OrganizationID", "dbo.Organizations", "ID");
            AddForeignKey("dbo.OrganizationMembers", "UserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Repositories", "UserId", "dbo.Users", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Repositories", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrganizationMembers", "UserID", "dbo.Users");
            DropForeignKey("dbo.OrganizationMembers", "OrganizationID", "dbo.Organizations");
            DropIndex("dbo.Repositories", new[] { "UserId" });
            DropIndex("dbo.OrganizationMembers", new[] { "OrganizationID" });
            DropIndex("dbo.OrganizationMembers", new[] { "UserID" });
            AlterColumn("dbo.Repositories", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrganizationMembers", "OrganizationID", c => c.Int(nullable: false));
            AlterColumn("dbo.OrganizationMembers", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Repositories", "UserId");
            CreateIndex("dbo.OrganizationMembers", "OrganizationID");
            CreateIndex("dbo.OrganizationMembers", "UserID");
            AddForeignKey("dbo.Repositories", "UserId", "dbo.Users", "ID", cascadeDelete: true);
            AddForeignKey("dbo.OrganizationMembers", "UserID", "dbo.Users", "ID", cascadeDelete: true);
            AddForeignKey("dbo.OrganizationMembers", "OrganizationID", "dbo.Organizations", "ID", cascadeDelete: true);
        }
    }
}
