namespace AcademicInformationService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAdmin : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2efa60ff-01bf-44c4-860b-34095fd286b0', N'admin@academicinfo.com', 0, N'AGpYyrZsIeW9dJhPUJUXL2YdKTdq6ItbFkruEKp1TpyGgGNXOu4zhpzUpGUwm0oQGA==', N'8e0eeea9-de9d-46f0-a3ca-ba1e331cbae4', NULL, 0, 0, NULL, 1, 0, N'admin@academicinfo.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a3199659-4272-49b8-83a1-4c2e555ed95d', N'Administrator')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2efa60ff-01bf-44c4-860b-34095fd286b0', N'a3199659-4272-49b8-83a1-4c2e555ed95d')
");
        }
        
        public override void Down()
        {
        }
    }
}
