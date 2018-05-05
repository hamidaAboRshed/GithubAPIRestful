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
                .Index(t => t.UserId);
            
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Repositories", new[] { "UserId" });
            DropTable("dbo.Repositories");
        }
    }
}
