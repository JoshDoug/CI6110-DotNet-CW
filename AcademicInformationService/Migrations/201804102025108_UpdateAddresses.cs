namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAddresses : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HomeAddresses", "AddressId");
            DropColumn("dbo.WorkAddresses", "AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkAddresses", "AddressId", c => c.Byte(nullable: false));
            AddColumn("dbo.HomeAddresses", "AddressId", c => c.Byte(nullable: false));
        }
    }
}
