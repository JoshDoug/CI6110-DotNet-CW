namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeMemberIdType : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE Members DROP CONSTRAINT [DF__Members__MemberI__71D1E811]");
            DropForeignKey("dbo.WorkAddresses", "MemberId", "dbo.Members");
            DropForeignKey("dbo.HomeAddresses", "MemberId", "dbo.Members");
            DropIndex("dbo.HomeAddresses", new[] { "MemberId" });
            DropIndex("dbo.WorkAddresses", new[] { "MemberId" });
            DropPrimaryKey("dbo.HomeAddresses");
            DropPrimaryKey("dbo.Members");
            DropPrimaryKey("dbo.WorkAddresses");
            AlterColumn("dbo.HomeAddresses", "MemberId", c => c.Int(nullable: false));
            AlterColumn("dbo.Members", "MemberId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.WorkAddresses", "MemberId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.HomeAddresses", "MemberId");
            AddPrimaryKey("dbo.Members", "MemberId");
            AddPrimaryKey("dbo.WorkAddresses", "MemberId");
            CreateIndex("dbo.HomeAddresses", "MemberId");
            CreateIndex("dbo.WorkAddresses", "MemberId");
            AddForeignKey("dbo.WorkAddresses", "MemberId", "dbo.Members", "MemberId");
            AddForeignKey("dbo.HomeAddresses", "MemberId", "dbo.Members", "MemberId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HomeAddresses", "MemberId", "dbo.Members");
            DropForeignKey("dbo.WorkAddresses", "MemberId", "dbo.Members");
            DropIndex("dbo.WorkAddresses", new[] { "MemberId" });
            DropIndex("dbo.HomeAddresses", new[] { "MemberId" });
            DropPrimaryKey("dbo.WorkAddresses");
            DropPrimaryKey("dbo.Members");
            DropPrimaryKey("dbo.HomeAddresses");
            AlterColumn("dbo.WorkAddresses", "MemberId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Members", "MemberId", c => c.Byte(nullable: false));
            AlterColumn("dbo.HomeAddresses", "MemberId", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.WorkAddresses", "MemberId");
            AddPrimaryKey("dbo.Members", "MemberId");
            AddPrimaryKey("dbo.HomeAddresses", "MemberId");
            CreateIndex("dbo.WorkAddresses", "MemberId");
            CreateIndex("dbo.HomeAddresses", "MemberId");
            AddForeignKey("dbo.HomeAddresses", "MemberId", "dbo.Members", "MemberId");
            AddForeignKey("dbo.WorkAddresses", "MemberId", "dbo.Members", "MemberId");
        }
    }
}
