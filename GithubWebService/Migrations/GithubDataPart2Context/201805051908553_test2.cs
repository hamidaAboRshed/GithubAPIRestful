namespace GithubWebService.Migrations.GithubDataPart2Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Repositories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 20),
                        Language = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        BillingEmail = c.String(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.OrganizationMembers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(),
                        OrganizationID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.OrganizationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Repositories", "UserId", "dbo.Users");
            DropForeignKey("dbo.Organizations", "User_ID", "dbo.Users");
            DropForeignKey("dbo.OrganizationMembers", "UserID", "dbo.Users");
            DropForeignKey("dbo.OrganizationMembers", "OrganizationID", "dbo.Organizations");
            DropIndex("dbo.OrganizationMembers", new[] { "OrganizationID" });
            DropIndex("dbo.OrganizationMembers", new[] { "UserID" });
            DropIndex("dbo.Organizations", new[] { "User_ID" });
            DropIndex("dbo.Repositories", new[] { "UserId" });
            DropTable("dbo.OrganizationMembers");
            DropTable("dbo.Organizations");
            DropTable("dbo.Users");
            DropTable("dbo.Repositories");
        }
    }
}
