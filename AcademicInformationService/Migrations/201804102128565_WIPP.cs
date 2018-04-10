namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WIPP : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Members", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.Members", "MembershipTypeId", "dbo.MembershipTypes");
            DropForeignKey("dbo.WorkAddresses", "MemberId", "dbo.Members");
            DropForeignKey("dbo.HomeAddresses", "MemberId", "dbo.Members");
            DropIndex("dbo.HomeAddresses", new[] { "MemberId" });
            DropIndex("dbo.Members", new[] { "GenderId" });
            DropIndex("dbo.Members", new[] { "MembershipTypeId" });
            DropIndex("dbo.WorkAddresses", new[] { "MemberId" });
            DropTable("dbo.HomeAddresses");
            DropTable("dbo.Members");
            DropTable("dbo.WorkAddresses");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.MemberId);
            
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
                .PrimaryKey(t => t.MemberId);
            
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
                .PrimaryKey(t => t.MemberId);
            
            CreateIndex("dbo.WorkAddresses", "MemberId");
            CreateIndex("dbo.Members", "MembershipTypeId");
            CreateIndex("dbo.Members", "GenderId");
            CreateIndex("dbo.HomeAddresses", "MemberId");
            AddForeignKey("dbo.HomeAddresses", "MemberId", "dbo.Members", "MemberId");
            AddForeignKey("dbo.WorkAddresses", "MemberId", "dbo.Members", "MemberId");
            AddForeignKey("dbo.Members", "MembershipTypeId", "dbo.MembershipTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Members", "GenderId", "dbo.Genders", "Id", cascadeDelete: true);
        }
    }
}
