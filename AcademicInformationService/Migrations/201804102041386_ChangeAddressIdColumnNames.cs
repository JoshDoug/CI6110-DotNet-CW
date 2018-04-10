namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAddressIdColumnNames : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.HomeAddresses");
            DropPrimaryKey("dbo.WorkAddresses");
            AddColumn("dbo.HomeAddresses", "Id", c => c.Byte(nullable: false));
            AddColumn("dbo.WorkAddresses", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.HomeAddresses", "Id");
            AddPrimaryKey("dbo.WorkAddresses", "Id");
            DropColumn("dbo.HomeAddresses", "HomeAddressId");
            DropColumn("dbo.WorkAddresses", "WorkAddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkAddresses", "WorkAddressId", c => c.Byte(nullable: false));
            AddColumn("dbo.HomeAddresses", "HomeAddressId", c => c.Byte(nullable: false));
            DropPrimaryKey("dbo.WorkAddresses");
            DropPrimaryKey("dbo.HomeAddresses");
            DropColumn("dbo.WorkAddresses", "Id");
            DropColumn("dbo.HomeAddresses", "Id");
            AddPrimaryKey("dbo.WorkAddresses", "WorkAddressId");
            AddPrimaryKey("dbo.HomeAddresses", "HomeAddressId");
        }
    }
}
