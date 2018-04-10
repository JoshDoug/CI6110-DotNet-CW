namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTablesBack : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HomeAddresses",
                c => new
                    {
                        MemberId = c.Int(nullable: false),
                        Building = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        TownCity = c.String(nullable: false),
                        County = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Birthdate = c.DateTime(),
                        Biography = c.String(),
                        GenderId = c.Byte(nullable: false),
                        MembershipTypeId = c.Byte(nullable: false),
                        Email = c.String(nullable: false),
                        HomeNumber = c.String(nullable: false),
                        WorkNumber = c.String(),
                        MobileNumber = c.String(),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.Genders", t => t.GenderId, cascadeDelete: true)
                .ForeignKey("dbo.MembershipTypes", t => t.MembershipTypeId, cascadeDelete: true)
                .Index(t => t.GenderId)
                .Index(t => t.MembershipTypeId);
            
            CreateTable(
                "dbo.WorkAddresses",
                c => new
                    {
                        MemberId = c.Int(nullable: false),
                        Building = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        TownCity = c.String(nullable: false),
                        County = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MemberId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .Index(t => t.MemberId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HomeAddresses", "MemberId", "dbo.Members");
            DropForeignKey("dbo.WorkAddresses", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Members", "MembershipTypeId", "dbo.MembershipTypes");
            DropForeignKey("dbo.Members", "GenderId", "dbo.Genders");
            DropIndex("dbo.WorkAddresses", new[] { "MemberId" });
            DropIndex("dbo.Members", new[] { "MembershipTypeId" });
            DropIndex("dbo.Members", new[] { "GenderId" });
            DropIndex("dbo.HomeAddresses", new[] { "MemberId" });
            DropTable("dbo.WorkAddresses");
            DropTable("dbo.Members");
            DropTable("dbo.HomeAddresses");
        }
    }
}
