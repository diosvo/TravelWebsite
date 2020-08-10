namespace TravelWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Login", "Password", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Services", "Image", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Services", "Image", c => c.String());
            AlterColumn("dbo.Login", "Password", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
