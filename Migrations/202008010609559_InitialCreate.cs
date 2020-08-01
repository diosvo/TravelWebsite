namespace TravelWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Login",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Package",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Offer = c.String(nullable: false, maxLength: 40),
                        Image = c.String(),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        paymentMode = c.String(),
                        ReID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Reservation", t => t.ReID, cascadeDelete: true)
                .Index(t => t.ReID);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ReID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Email = c.String(nullable: false, maxLength: 20),
                        Offers = c.String(nullable: false, maxLength: 40),
                        Services = c.String(nullable: false, maxLength: 40),
                        phoneNumber = c.String(nullable: false, maxLength: 10),
                        NIC = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.ReID);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Service = c.String(nullable: false, maxLength: 40),
                        Image = c.String(),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payment", "ReID", "dbo.Reservation");
            DropIndex("dbo.Payment", new[] { "ReID" });
            DropTable("dbo.Services");
            DropTable("dbo.Reservation");
            DropTable("dbo.Payment");
            DropTable("dbo.Package");
            DropTable("dbo.Login");
        }
    }
}
