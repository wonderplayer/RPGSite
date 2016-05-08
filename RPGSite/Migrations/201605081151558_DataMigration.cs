namespace RPGSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        RecordID = c.Int(nullable: false, identity: true),
                        CartID = c.String(),
                        EquipmentID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecordID)
                .ForeignKey("dbo.Equipments", t => t.EquipmentID, cascadeDelete: true)
                .Index(t => t.EquipmentID);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false, maxLength: 500),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Picture = c.String(nullable: false),
                        TypeID = c.Int(nullable: false),
                        RarityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EquipmentRarities", t => t.RarityID, cascadeDelete: true)
                .ForeignKey("dbo.EquipmentTypes", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.TypeID)
                .Index(t => t.RarityID);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        EquipmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Equipments", t => t.EquipmentID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.EquipmentID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 255),
                        Created = c.DateTime(nullable: false),
                        PostID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.PostID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 3000),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        IsNews = c.Boolean(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 1000),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Picture = c.String(),
                        Description = c.String(maxLength: 100),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OfferedItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TradeID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Equipments", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.Trades", t => t.TradeID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.TradeID)
                .Index(t => t.ItemID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Trades",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TradeDate = c.DateTime(nullable: false),
                        Status = c.String(maxLength: 8),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WantedItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TradeID = c.Int(nullable: false),
                        ItemID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Equipments", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.Trades", t => t.TradeID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.TradeID)
                .Index(t => t.ItemID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserID = c.String(maxLength: 128),
                        PaymentMethodID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethodID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.PaymentMethodID);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EquipmentID = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Equipments", t => t.EquipmentID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.EquipmentID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Method = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecievedMessages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SentMessageID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SentMessages", t => t.SentMessageID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.SentMessageID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.SentMessages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Message = c.String(nullable: false, maxLength: 500),
                        DateSent = c.DateTime(nullable: false),
                        Read = c.Boolean(nullable: false),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.EquipmentRarities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Rarity = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EquipmentTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Carts", "EquipmentID", "dbo.Equipments");
            DropForeignKey("dbo.Equipments", "TypeID", "dbo.EquipmentTypes");
            DropForeignKey("dbo.Equipments", "RarityID", "dbo.EquipmentRarities");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RecievedMessages", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.SentMessages", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.RecievedMessages", "SentMessageID", "dbo.SentMessages");
            DropForeignKey("dbo.Orders", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "PaymentMethodID", "dbo.PaymentMethods");
            DropForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "EquipmentID", "dbo.Equipments");
            DropForeignKey("dbo.OfferedItems", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.WantedItems", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.WantedItems", "TradeID", "dbo.Trades");
            DropForeignKey("dbo.WantedItems", "ItemID", "dbo.Equipments");
            DropForeignKey("dbo.OfferedItems", "TradeID", "dbo.Trades");
            DropForeignKey("dbo.OfferedItems", "ItemID", "dbo.Equipments");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Inventories", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Galleries", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Events", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Inventories", "EquipmentID", "dbo.Equipments");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.SentMessages", new[] { "UserID" });
            DropIndex("dbo.RecievedMessages", new[] { "UserID" });
            DropIndex("dbo.RecievedMessages", new[] { "SentMessageID" });
            DropIndex("dbo.OrderItems", new[] { "OrderID" });
            DropIndex("dbo.OrderItems", new[] { "EquipmentID" });
            DropIndex("dbo.Orders", new[] { "PaymentMethodID" });
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropIndex("dbo.WantedItems", new[] { "UserID" });
            DropIndex("dbo.WantedItems", new[] { "ItemID" });
            DropIndex("dbo.WantedItems", new[] { "TradeID" });
            DropIndex("dbo.OfferedItems", new[] { "UserID" });
            DropIndex("dbo.OfferedItems", new[] { "ItemID" });
            DropIndex("dbo.OfferedItems", new[] { "TradeID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Galleries", new[] { "UserID" });
            DropIndex("dbo.Events", new[] { "UserID" });
            DropIndex("dbo.Posts", new[] { "UserID" });
            DropIndex("dbo.Comments", new[] { "UserID" });
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Inventories", new[] { "EquipmentID" });
            DropIndex("dbo.Inventories", new[] { "UserID" });
            DropIndex("dbo.Equipments", new[] { "RarityID" });
            DropIndex("dbo.Equipments", new[] { "TypeID" });
            DropIndex("dbo.Carts", new[] { "EquipmentID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EquipmentTypes");
            DropTable("dbo.EquipmentRarities");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.SentMessages");
            DropTable("dbo.RecievedMessages");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Orders");
            DropTable("dbo.WantedItems");
            DropTable("dbo.Trades");
            DropTable("dbo.OfferedItems");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Galleries");
            DropTable("dbo.Events");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Inventories");
            DropTable("dbo.Equipments");
            DropTable("dbo.Carts");
        }
    }
}
