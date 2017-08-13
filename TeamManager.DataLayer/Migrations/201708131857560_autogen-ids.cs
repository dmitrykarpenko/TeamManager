namespace TeamManager.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autogenids : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamPlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.TeamCourses", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamPlayers", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamCourses", "CourseId", "dbo.Courses");
            DropPrimaryKey("dbo.Players");
            DropPrimaryKey("dbo.Teams");
            DropPrimaryKey("dbo.Courses");
            AlterColumn("dbo.Players", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Teams", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Courses", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Players", "Id");
            AddPrimaryKey("dbo.Teams", "Id");
            AddPrimaryKey("dbo.Courses", "Id");
            AddForeignKey("dbo.TeamPlayers", "PlayerId", "dbo.Players", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamCourses", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamPlayers", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamCourses", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            DropColumn("dbo.Players", "TeamId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "TeamId", c => c.Guid());
            DropForeignKey("dbo.TeamCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.TeamPlayers", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamCourses", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamPlayers", "PlayerId", "dbo.Players");
            DropPrimaryKey("dbo.Courses");
            DropPrimaryKey("dbo.Teams");
            DropPrimaryKey("dbo.Players");
            AlterColumn("dbo.Courses", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Teams", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Players", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Courses", "Id");
            AddPrimaryKey("dbo.Teams", "Id");
            AddPrimaryKey("dbo.Players", "Id");
            AddForeignKey("dbo.TeamCourses", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamPlayers", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamCourses", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TeamPlayers", "PlayerId", "dbo.Players", "Id", cascadeDelete: true);
        }
    }
}
