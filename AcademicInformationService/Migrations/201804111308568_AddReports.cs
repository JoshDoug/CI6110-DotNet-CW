namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ReportId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        ReportDate = c.DateTime(nullable: false),
                        Abstract = c.String(),
                        ReportText = c.String(),
                    })
                .PrimaryKey(t => t.ReportId);
            
            CreateTable(
                "dbo.ReportMembers",
                c => new
                    {
                        Report_ReportId = c.Int(nullable: false),
                        Member_MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Report_ReportId, t.Member_MemberId })
                .ForeignKey("dbo.Reports", t => t.Report_ReportId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.Member_MemberId, cascadeDelete: true)
                .Index(t => t.Report_ReportId)
                .Index(t => t.Member_MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportMembers", "Member_MemberId", "dbo.Members");
            DropForeignKey("dbo.ReportMembers", "Report_ReportId", "dbo.Reports");
            DropIndex("dbo.ReportMembers", new[] { "Member_MemberId" });
            DropIndex("dbo.ReportMembers", new[] { "Report_ReportId" });
            DropTable("dbo.ReportMembers");
            DropTable("dbo.Reports");
        }
    }
}
