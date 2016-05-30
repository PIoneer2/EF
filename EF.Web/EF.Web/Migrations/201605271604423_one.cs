namespace EF.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AccessFailedCount = c.Int(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        LockoutEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        PasswordHash = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        SecurityStamp = c.String(),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        UserName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoleId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        TranactionTypeId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.TranactionType", t => t.TranactionTypeId, cascadeDelete: true)
                .Index(t => t.TranactionTypeId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.GoodsInTransaction",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        TransactionsId = c.Long(nullable: false),
                        GoodsId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Transactions", t => t.TransactionsId, cascadeDelete: true)
                .ForeignKey("dbo.Goods", t => t.GoodsId, cascadeDelete: true)
                .Index(t => t.TransactionsId)
                .Index(t => t.GoodsId);
            
            CreateTable(
                "dbo.GoodsInWarehauses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GoodsId = c.Long(nullable: false),
                        WarehousesPlacesId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.GoodsId, cascadeDelete: true)
                .ForeignKey("dbo.WarehousesPlaces", t => t.WarehousesPlacesId, cascadeDelete: true)
                .Index(t => t.GoodsId)
                .Index(t => t.WarehousesPlacesId);
            
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Quantity = c.Int(nullable: false),
                        TypeOfStorageId = c.Long(nullable: false),
                        SizesId = c.Long(nullable: false),
                        Info = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sizes", t => t.SizesId, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfStorage", t => t.TypeOfStorageId, cascadeDelete: true)
                .Index(t => t.TypeOfStorageId)
                .Index(t => t.SizesId);
            
            CreateTable(
                "dbo.RestrictionsSet",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RestrictionsId = c.Long(nullable: false),
                        GoodsId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.GoodsId, cascadeDelete: true)
                .ForeignKey("dbo.Restrictions", t => t.RestrictionsId, cascadeDelete: true)
                .Index(t => t.RestrictionsId)
                .Index(t => t.GoodsId);
            
            CreateTable(
                "dbo.Restrictions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RestrictionName = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Size = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TranactionType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeOfStorage",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WarehousesPlaces",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Adress = c.String(nullable: false),
                        Place = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GoodsInWarehauses", "WarehousesPlacesId", "dbo.WarehousesPlaces");
            DropForeignKey("dbo.Goods", "TypeOfStorageId", "dbo.TypeOfStorage");
            DropForeignKey("dbo.Transactions", "TranactionTypeId", "dbo.TranactionType");
            DropForeignKey("dbo.Goods", "SizesId", "dbo.Sizes");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RestrictionsSet", "RestrictionsId", "dbo.Restrictions");
            DropForeignKey("dbo.RestrictionsSet", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.GoodsInWarehauses", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.GoodsInTransaction", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.Transactions", "UserId", "dbo.Users");
            DropForeignKey("dbo.GoodsInTransaction", "TransactionsId", "dbo.Transactions");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Logins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Claims", "UserId", "dbo.Users");
            DropIndex("dbo.RestrictionsSet", new[] { "GoodsId" });
            DropIndex("dbo.RestrictionsSet", new[] { "RestrictionsId" });
            DropIndex("dbo.Goods", new[] { "SizesId" });
            DropIndex("dbo.Goods", new[] { "TypeOfStorageId" });
            DropIndex("dbo.GoodsInWarehauses", new[] { "WarehousesPlacesId" });
            DropIndex("dbo.GoodsInWarehauses", new[] { "GoodsId" });
            DropIndex("dbo.GoodsInTransaction", new[] { "GoodsId" });
            DropIndex("dbo.GoodsInTransaction", new[] { "TransactionsId" });
            DropIndex("dbo.Transactions", new[] { "UserId" });
            DropIndex("dbo.Transactions", new[] { "TranactionTypeId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.Logins", new[] { "UserId" });
            DropIndex("dbo.Claims", new[] { "UserId" });
            DropTable("dbo.WarehousesPlaces");
            DropTable("dbo.TypeOfStorage");
            DropTable("dbo.TranactionType");
            DropTable("dbo.Sizes");
            DropTable("dbo.Roles");
            DropTable("dbo.Restrictions");
            DropTable("dbo.RestrictionsSet");
            DropTable("dbo.Goods");
            DropTable("dbo.GoodsInWarehauses");
            DropTable("dbo.GoodsInTransaction");
            DropTable("dbo.Transactions");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Logins");
            DropTable("dbo.Claims");
            DropTable("dbo.Users");
        }
    }
}
