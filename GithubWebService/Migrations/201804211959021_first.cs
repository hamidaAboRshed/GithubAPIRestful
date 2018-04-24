namespace GithubWebService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
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
                        UserID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.OrganizationID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrganizationMembers", "UserID", "dbo.Users");
            DropForeignKey("dbo.Repositories", "UserId", "dbo.Users");
            DropForeignKey("dbo.Organizations", "User_ID", "dbo.Users");
            DropForeignKey("dbo.OrganizationMembers", "OrganizationID", "dbo.Organizations");
            DropIndex("dbo.Repositories", new[] { "UserId" });
            DropIndex("dbo.OrganizationMembers", new[] { "OrganizationID" });
            DropIndex("dbo.OrganizationMembers", new[] { "UserID" });
            DropIndex("dbo.Organizations", new[] { "User_ID" });
            DropTable("dbo.Repositories");
            DropTable("dbo.Users");
            DropTable("dbo.OrganizationMembers");
            DropTable("dbo.Organizations");
        }
    }
}
