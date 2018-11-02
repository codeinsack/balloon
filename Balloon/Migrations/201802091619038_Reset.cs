namespace Balloon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reset : DbMigration
    {
        public override void Up()
        {
        //    CreateTable(
        //        "dbo.Articles",
        //        c => new
        //            {
        //                ArticleId = c.Int(nullable: false, identity: true),
        //                Title = c.String(),
        //                Date = c.DateTime(nullable: false),
        //                Content = c.String(),
        //                UserId = c.String(maxLength: 128),
        //            })
        //        .PrimaryKey(t => t.ArticleId)
        //        .ForeignKey("dbo.AspNetUsers", t => t.UserId)
        //        .Index(t => t.UserId);
            
        //    CreateTable(
        //        "dbo.AspNetUsers",
        //        c => new
        //            {
        //                Id = c.String(nullable: false, maxLength: 128),
        //                NickName = c.String(),
        //                Email = c.String(maxLength: 256),
        //                EmailConfirmed = c.Boolean(nullable: false),
        //                PasswordHash = c.String(),
        //                SecurityStamp = c.String(),
        //                PhoneNumber = c.String(),
        //                PhoneNumberConfirmed = c.Boolean(nullable: false),
        //                TwoFactorEnabled = c.Boolean(nullable: false),
        //                LockoutEndDateUtc = c.DateTime(),
        //                LockoutEnabled = c.Boolean(nullable: false),
        //                AccessFailedCount = c.Int(nullable: false),
        //                UserName = c.String(nullable: false, maxLength: 256),
        //            })
        //        .PrimaryKey(t => t.Id)
        //        .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
        //    CreateTable(
        //        "dbo.AspNetUserClaims",
        //        c => new
        //            {
        //                Id = c.Int(nullable: false, identity: true),
        //                UserId = c.String(nullable: false, maxLength: 128),
        //                ClaimType = c.String(),
        //                ClaimValue = c.String(),
        //            })
        //        .PrimaryKey(t => t.Id)
        //        .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
        //        .Index(t => t.UserId);
            
        //    CreateTable(
        //        "dbo.AspNetUserLogins",
        //        c => new
        //            {
        //                LoginProvider = c.String(nullable: false, maxLength: 128),
        //                ProviderKey = c.String(nullable: false, maxLength: 128),
        //                UserId = c.String(nullable: false, maxLength: 128),
        //            })
        //        .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
        //        .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
        //        .Index(t => t.UserId);
            
        //    CreateTable(
        //        "dbo.Orders",
        //        c => new
        //            {
        //                OrderId = c.Int(nullable: false, identity: true),
        //                Date = c.DateTime(nullable: false),
        //                TotalSum = c.Decimal(nullable: false, precision: 18, scale: 2),
        //                Delivery = c.Boolean(nullable: false),
        //                DeliveryDate = c.DateTime(nullable: false),
        //                DeliveryAddress = c.String(),
        //                UserId = c.String(maxLength: 128),
        //            })
        //        .PrimaryKey(t => t.OrderId)
        //        .ForeignKey("dbo.AspNetUsers", t => t.UserId)
        //        .Index(t => t.UserId);
            
        //    CreateTable(
        //        "dbo.OrderItems",
        //        c => new
        //            {
        //                OrderItemId = c.Int(nullable: false, identity: true),
        //                Price = c.Decimal(nullable: false, precision: 18, scale: 2),
        //                Quantity = c.Int(nullable: false),
        //                GoodId = c.Int(nullable: false),
        //                OrderId = c.Int(nullable: false),
        //            })
        //        .PrimaryKey(t => t.OrderItemId)
        //        .ForeignKey("dbo.Goods", t => t.GoodId, cascadeDelete: true)
        //        .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
        //        .Index(t => t.GoodId)
        //        .Index(t => t.OrderId);
            
        //    CreateTable(
        //        "dbo.Goods",
        //        c => new
        //            {
        //                GoodId = c.Int(nullable: false, identity: true),
        //                Name = c.String(nullable: false),
        //                Price = c.Decimal(nullable: false, precision: 18, scale: 2),
        //                ImageData = c.Binary(),
        //                ImageMimeType = c.String(),
        //                CategoryId = c.Int(nullable: false),
        //            })
        //        .PrimaryKey(t => t.GoodId)
        //        .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
        //        .Index(t => t.CategoryId);
            
        //    CreateTable(
        //        "dbo.Categories",
        //        c => new
        //            {
        //                CategoryId = c.Int(nullable: false, identity: true),
        //                Name = c.String(),
        //            })
        //        .PrimaryKey(t => t.CategoryId);
            
        //    CreateTable(
        //        "dbo.AspNetUserRoles",
        //        c => new
        //            {
        //                UserId = c.String(nullable: false, maxLength: 128),
        //                RoleId = c.String(nullable: false, maxLength: 128),
        //            })
        //        .PrimaryKey(t => new { t.UserId, t.RoleId })
        //        .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
        //        .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
        //        .Index(t => t.UserId)
        //        .Index(t => t.RoleId);
            
        //    CreateTable(
        //        "dbo.AspNetRoles",
        //        c => new
        //            {
        //                Id = c.String(nullable: false, maxLength: 128),
        //                Name = c.String(nullable: false, maxLength: 256),
        //                Description = c.String(),
        //                Discriminator = c.String(nullable: false, maxLength: 128),
        //            })
        //        .PrimaryKey(t => t.Id)
        //        .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "GoodId", "dbo.Goods");
            DropForeignKey("dbo.Goods", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Articles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Goods", new[] { "CategoryId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.OrderItems", new[] { "GoodId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Categories");
            DropTable("dbo.Goods");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Articles");
        }
    }
}
