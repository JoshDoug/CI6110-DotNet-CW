namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypes(Id, Name) VALUES (1, 'Normal')");
            Sql("INSERT INTO MembershipTypes(Id, Name) VALUES (2, 'Chair')");
            Sql("INSERT INTO MembershipTypes(Id, Name) VALUES (3, 'Co-Chair')");
        }
        
        public override void Down()
        {
        }
    }
}
