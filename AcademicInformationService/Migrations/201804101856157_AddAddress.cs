namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddress : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Members", t => t.Member_Id)
                .Index(t => t.Member_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "Member_Id", "dbo.Members");
            DropIndex("dbo.Addresses", new[] { "Member_Id" });
            DropTable("dbo.Addresses");
        }
    }
}
