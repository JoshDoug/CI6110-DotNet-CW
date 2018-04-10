namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WIP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genders", t => t.GenderId, cascadeDelete: true)
                .ForeignKey("dbo.MembershipTypes", t => t.MembershipTypeId, cascadeDelete: true)
                .Index(t => t.GenderId)
                .Index(t => t.MembershipTypeId);
            
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "MembershipTypeId", "dbo.MembershipTypes");
            DropForeignKey("dbo.Members", "GenderId", "dbo.Genders");
            DropIndex("dbo.Members", new[] { "MembershipTypeId" });
            DropIndex("dbo.Members", new[] { "GenderId" });
            DropTable("dbo.MembershipTypes");
            DropTable("dbo.Members");
            DropTable("dbo.Genders");
        }
    }
}
