namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventLocations",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        Building = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        TownCity = c.String(nullable: false),
                        County = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Events", t => t.EventId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        EventDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.MemberEvents",
                c => new
                    {
                        Member_MemberId = c.Int(nullable: false),
                        Event_EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Member_MemberId, t.Event_EventId })
                .ForeignKey("dbo.Members", t => t.Member_MemberId, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_EventId, cascadeDelete: true)
                .Index(t => t.Member_MemberId)
                .Index(t => t.Event_EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventLocations", "EventId", "dbo.Events");
            DropForeignKey("dbo.MemberEvents", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.MemberEvents", "Member_MemberId", "dbo.Members");
            DropIndex("dbo.MemberEvents", new[] { "Event_EventId" });
            DropIndex("dbo.MemberEvents", new[] { "Member_MemberId" });
            DropIndex("dbo.EventLocations", new[] { "EventId" });
            DropTable("dbo.MemberEvents");
            DropTable("dbo.Events");
            DropTable("dbo.EventLocations");
        }
    }
}
