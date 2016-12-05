namespace WebApplicationLab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Temp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Batteries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cameras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OZUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CompanyId = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Image = c.String(),
                        Description = c.String(),
                        SystemOperationId = c.Int(nullable: false),
                        ScreenSizeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        OZUId = c.Int(nullable: false),
                        ColorId = c.Int(nullable: false),
                        CameraId = c.Int(nullable: false),
                        BatteryId = c.Int(nullable: false),
                        Memory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Batteries", t => t.BatteryId, cascadeDelete: true)
                .ForeignKey("dbo.Cameras", t => t.CameraId, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.ColorId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.OZUs", t => t.OZUId, cascadeDelete: true)
                .ForeignKey("dbo.ScreenSizes", t => t.ScreenSizeId, cascadeDelete: true)
                .ForeignKey("dbo.SystemOperations", t => t.SystemOperationId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.SystemOperationId)
                .Index(t => t.ScreenSizeId)
                .Index(t => t.OZUId)
                .Index(t => t.ColorId)
                .Index(t => t.CameraId)
                .Index(t => t.BatteryId);
            
            CreateTable(
                "dbo.ScreenSizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SystemOperations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 128),
                        Salt = c.String(),
                        RoleId = c.Int(nullable: false),
                        Telephone = c.String(),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Phones", "SystemOperationId", "dbo.SystemOperations");
            DropForeignKey("dbo.Phones", "ScreenSizeId", "dbo.ScreenSizes");
            DropForeignKey("dbo.Phones", "OZUId", "dbo.OZUs");
            DropForeignKey("dbo.Phones", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Phones", "ColorId", "dbo.Colors");
            DropForeignKey("dbo.Phones", "CameraId", "dbo.Cameras");
            DropForeignKey("dbo.Phones", "BatteryId", "dbo.Batteries");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Phones", new[] { "BatteryId" });
            DropIndex("dbo.Phones", new[] { "CameraId" });
            DropIndex("dbo.Phones", new[] { "ColorId" });
            DropIndex("dbo.Phones", new[] { "OZUId" });
            DropIndex("dbo.Phones", new[] { "ScreenSizeId" });
            DropIndex("dbo.Phones", new[] { "SystemOperationId" });
            DropIndex("dbo.Phones", new[] { "CompanyId" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.SystemOperations");
            DropTable("dbo.ScreenSizes");
            DropTable("dbo.Phones");
            DropTable("dbo.OZUs");
            DropTable("dbo.Companies");
            DropTable("dbo.Colors");
            DropTable("dbo.Cameras");
            DropTable("dbo.Batteries");
        }
    }
}
