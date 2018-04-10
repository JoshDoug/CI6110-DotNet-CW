namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAddressIdColumnNamesAgain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WorkAddresses", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.HomeAddresses", "Member_Id", "dbo.Members");
            DropIndex("dbo.HomeAddresses", new[] { "Member_Id" });
            DropIndex("dbo.WorkAddresses", new[] { "Member_Id" });
            RenameColumn(table: "dbo.HomeAddresses", name: "Member_Id", newName: "MemberId");
            RenameColumn(table: "dbo.WorkAddresses", name: "Member_Id", newName: "MemberId");
            DropPrimaryKey("dbo.HomeAddresses");
            DropPrimaryKey("dbo.Members");
            DropPrimaryKey("dbo.WorkAddresses");
            AddColumn("dbo.Members", "MemberId", c => c.Byte(nullable: false));
            AlterColumn("dbo.HomeAddresses", "MemberId", c => c.Byte(nullable: false));
            AlterColumn("dbo.WorkAddresses", "MemberId", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.HomeAddresses", "MemberId");
            AddPrimaryKey("dbo.Members", "MemberId");
            AddPrimaryKey("dbo.WorkAddresses", "MemberId");
            CreateIndex("dbo.HomeAddresses", "MemberId");
            CreateIndex("dbo.WorkAddresses", "MemberId");
            AddForeignKey("dbo.WorkAddresses", "MemberId", "dbo.Members", "MemberId");
            AddForeignKey("dbo.HomeAddresses", "MemberId", "dbo.Members", "MemberId");
            DropColumn("dbo.HomeAddresses", "Id");
            DropColumn("dbo.Members", "Id");
            DropColumn("dbo.WorkAddresses", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkAddresses", "Id", c => c.Byte(nullable: false));
            AddColumn("dbo.Members", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.HomeAddresses", "Id", c => c.Byte(nullable: false));
            DropForeignKey("dbo.HomeAddresses", "MemberId", "dbo.Members");
            DropForeignKey("dbo.WorkAddresses", "MemberId", "dbo.Members");
            DropIndex("dbo.WorkAddresses", new[] { "MemberId" });
            DropIndex("dbo.HomeAddresses", new[] { "MemberId" });
            DropPrimaryKey("dbo.WorkAddresses");
            DropPrimaryKey("dbo.Members");
            DropPrimaryKey("dbo.HomeAddresses");
            AlterColumn("dbo.WorkAddresses", "MemberId", c => c.Int(nullable: false));
            AlterColumn("dbo.HomeAddresses", "MemberId", c => c.Int(nullable: false));
            DropColumn("dbo.Members", "MemberId");
            AddPrimaryKey("dbo.WorkAddresses", "Id");
            AddPrimaryKey("dbo.Members", "Id");
            AddPrimaryKey("dbo.HomeAddresses", "Id");
            RenameColumn(table: "dbo.WorkAddresses", name: "MemberId", newName: "Member_Id");
            RenameColumn(table: "dbo.HomeAddresses", name: "MemberId", newName: "Member_Id");
            CreateIndex("dbo.WorkAddresses", "Member_Id");
            CreateIndex("dbo.HomeAddresses", "Member_Id");
            AddForeignKey("dbo.HomeAddresses", "Member_Id", "dbo.Members", "Id");
            AddForeignKey("dbo.WorkAddresses", "Member_Id", "dbo.Members", "Id");
        }
    }
}
