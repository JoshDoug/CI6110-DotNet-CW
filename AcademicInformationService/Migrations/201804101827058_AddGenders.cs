namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenders : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genders(Id, Type) VALUES (1, 'Male')");
            Sql("INSERT INTO Genders(Id, Type) VALUES (2, 'Female')");
            Sql("INSERT INTO Genders(Id, Type) VALUES (3, 'Other')");
        }
        
        public override void Down()
        {
        }
    }
}
