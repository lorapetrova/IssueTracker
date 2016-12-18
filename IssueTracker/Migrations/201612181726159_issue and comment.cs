namespace IssueTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class issueandcomment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueId = c.Int(nullable: false),
                        AuthorName = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Issues", t => t.IssueId, cascadeDelete: true)
                .Index(t => t.IssueId);
            
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        AuthorId = c.String(maxLength: 128),
                        State = c.Int(nullable: false),
                        SubmitionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.Issues", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Issues", new[] { "AuthorId" });
            DropIndex("dbo.Comments", new[] { "IssueId" });
            DropTable("dbo.Issues");
            DropTable("dbo.Comments");
        }
    }
}
