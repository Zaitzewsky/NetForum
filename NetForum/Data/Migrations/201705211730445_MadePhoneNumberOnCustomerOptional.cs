namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadePhoneNumberOnCustomerOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
