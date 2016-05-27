namespace EF.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        TranactionTypeId = c.Long(nullable: false),
                        AspNetUsersId = c.String(),
                        Date = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TranactionTypes", t => t.TranactionTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.TranactionTypeId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.GoodsInTransactions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        TransactionsId = c.Long(nullable: false),
                        GoodsId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.GoodsId, cascadeDelete: true)
                .ForeignKey("dbo.Transactions", t => t.TransactionsId, cascadeDelete: true)
                .Index(t => t.TransactionsId)
                .Index(t => t.GoodsId);
            
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        TypeOfStorageId = c.Long(nullable: false),
                        SizesId = c.Long(nullable: false),
                        Info = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sizes", t => t.SizesId, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfStorages", t => t.TypeOfStorageId, cascadeDelete: true)
                .Index(t => t.TypeOfStorageId)
                .Index(t => t.SizesId);
            
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
                "dbo.WarehousesPlaces",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Adress = c.String(),
                        Place = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RestrictionsSets",
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
                        RestrictionName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Size = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeOfStorages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TranactionTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "TranactionTypeId", "dbo.TranactionTypes");
            DropForeignKey("dbo.GoodsInTransactions", "TransactionsId", "dbo.Transactions");
            DropForeignKey("dbo.Goods", "TypeOfStorageId", "dbo.TypeOfStorages");
            DropForeignKey("dbo.Goods", "SizesId", "dbo.Sizes");
            DropForeignKey("dbo.RestrictionsSets", "RestrictionsId", "dbo.Restrictions");
            DropForeignKey("dbo.RestrictionsSets", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.GoodsInWarehauses", "WarehousesPlacesId", "dbo.WarehousesPlaces");
            DropForeignKey("dbo.GoodsInWarehauses", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.GoodsInTransactions", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.RestrictionsSets", new[] { "GoodsId" });
            DropIndex("dbo.RestrictionsSets", new[] { "RestrictionsId" });
            DropIndex("dbo.GoodsInWarehauses", new[] { "WarehousesPlacesId" });
            DropIndex("dbo.GoodsInWarehauses", new[] { "GoodsId" });
            DropIndex("dbo.Goods", new[] { "SizesId" });
            DropIndex("dbo.Goods", new[] { "TypeOfStorageId" });
            DropIndex("dbo.GoodsInTransactions", new[] { "GoodsId" });
            DropIndex("dbo.GoodsInTransactions", new[] { "TransactionsId" });
            DropIndex("dbo.Transactions", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Transactions", new[] { "TranactionTypeId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.TranactionTypes");
            DropTable("dbo.TypeOfStorages");
            DropTable("dbo.Sizes");
            DropTable("dbo.Restrictions");
            DropTable("dbo.RestrictionsSets");
            DropTable("dbo.WarehousesPlaces");
            DropTable("dbo.GoodsInWarehauses");
            DropTable("dbo.Goods");
            DropTable("dbo.GoodsInTransactions");
            DropTable("dbo.Transactions");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
