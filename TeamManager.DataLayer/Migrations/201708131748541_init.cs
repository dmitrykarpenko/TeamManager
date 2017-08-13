namespace TeamManager.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TeamId = c.Guid(),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamCourses",
                c => new
                    {
                        TeamId = c.Guid(nullable: false),
                        CourseId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamId, t.CourseId })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.TeamPlayers",
                c => new
                    {
                        TeamId = c.Guid(nullable: false),
                        PlayerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamId, t.PlayerId })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamPlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.TeamPlayers", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.TeamCourses", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamPlayers", new[] { "PlayerId" });
            DropIndex("dbo.TeamPlayers", new[] { "TeamId" });
            DropIndex("dbo.TeamCourses", new[] { "CourseId" });
            DropIndex("dbo.TeamCourses", new[] { "TeamId" });
            DropTable("dbo.TeamPlayers");
            DropTable("dbo.TeamCourses");
            DropTable("dbo.Courses");
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
        }
    }
}
