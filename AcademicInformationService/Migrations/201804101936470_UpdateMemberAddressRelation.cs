namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMemberAddressRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "Member_Id", "dbo.Members");
            DropIndex("dbo.Addresses", new[] { "Member_Id" });
            CreateTable(
                "dbo.HomeAddresses",
                c => new
                    {
                        HomeAddressId = c.Byte(nullable: false),
                        AddressId = c.Byte(nullable: false),
                        Building = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        TownCity = c.String(nullable: false),
                        County = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HomeAddressId)
                .ForeignKey("dbo.Members", t => t.Member_Id)
                .Index(t => t.Member_Id);
            
            CreateTable(
                "dbo.WorkAddresses",
                c => new
                    {
                        WorkAddressId = c.Byte(nullable: false),
                        AddressId = c.Byte(nullable: false),
                        Building = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        TownCity = c.String(nullable: false),
                        County = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkAddressId)
                .ForeignKey("dbo.Members", t => t.Member_Id)
                .Index(t => t.Member_Id);
            
            DropTable("dbo.Addresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Byte(nullable: false),
                        Type = c.String(),
                        Building = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        TownCity = c.String(nullable: false),
                        County = c.String(nullable: false),
                        Postcode = c.String(nullable: false),
                        Member_Id = c.Int(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            DropForeignKey("dbo.WorkAddresses", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.HomeAddresses", "Member_Id", "dbo.Members");
            DropIndex("dbo.WorkAddresses", new[] { "Member_Id" });
            DropIndex("dbo.HomeAddresses", new[] { "Member_Id" });
            DropTable("dbo.WorkAddresses");
            DropTable("dbo.HomeAddresses");
            CreateIndex("dbo.Addresses", "Member_Id");
            AddForeignKey("dbo.Addresses", "Member_Id", "dbo.Members", "Id");
        }
    }
}
