namespace TeamManager.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeredundantentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamCourses", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.TeamCourses", new[] { "TeamId" });
            DropIndex("dbo.TeamCourses", new[] { "CourseId" });
            DropTable("dbo.Courses");
            DropTable("dbo.TeamCourses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeamCourses",
                c => new
                    {
                        TeamId = c.Guid(nullable: false),
                        CourseId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamId, t.CourseId });
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.TeamCourses", "CourseId");
            CreateIndex("dbo.TeamCourses", "TeamId");
            AddForeignKey("dbo.TeamCourses", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamCourses", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
        }
    }
}
