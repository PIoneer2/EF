namespace EF.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    AspNetUsersId = c.Long(nullable: false),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUsersId)
                .Index(t => t.AspNetUsersId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AspNetUsersId = c.Long(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUsersId)
                .Index(t => t.AspNetUsersId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AspNetUsersId = c.Long(nullable: false),
                        CustomRoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUsersId)
                .ForeignKey("dbo.AspNetRoles", t => t.CustomRoleId)
                .Index(t => t.AspNetUsersId)
                .Index(t => t.CustomRoleId)
                ;
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        TranactionTypeId = c.Long(nullable: false),
                        AspNetUsersId = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUsersId, cascadeDelete: true)
                .ForeignKey("dbo.TranactionType", t => t.TranactionTypeId, cascadeDelete: true)
                .Index(t => t.TranactionTypeId)
                .Index(t => t.AspNetUsersId);
            
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
                "dbo.AspNetRoles",
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
            DropForeignKey("dbo.AspNetUserRoles", "CustomRole_Id1", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "CustomRole_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.RestrictionsSet", "RestrictionsId", "dbo.Restrictions");
            DropForeignKey("dbo.RestrictionsSet", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.GoodsInWarehauses", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.GoodsInTransaction", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.Transactions", "AspNetUsersId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GoodsInTransaction", "TransactionsId", "dbo.Transactions");
            DropForeignKey("dbo.AspNetUserRoles", "AspNetUsers_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "AspNetUsers_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "AspNetUsers_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "AspNetUsers_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "AspNetUsers_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "AspNetUsers_Id", "dbo.AspNetUsers");
            DropIndex("dbo.RestrictionsSet", new[] { "GoodsId" });
            DropIndex("dbo.RestrictionsSet", new[] { "RestrictionsId" });
            DropIndex("dbo.Goods", new[] { "SizesId" });
            DropIndex("dbo.Goods", new[] { "TypeOfStorageId" });
            DropIndex("dbo.GoodsInWarehauses", new[] { "WarehousesPlacesId" });
            DropIndex("dbo.GoodsInWarehauses", new[] { "GoodsId" });
            DropIndex("dbo.GoodsInTransaction", new[] { "GoodsId" });
            DropIndex("dbo.GoodsInTransaction", new[] { "TransactionsId" });
            DropIndex("dbo.Transactions", new[] { "AspNetUsersId" });
            DropIndex("dbo.Transactions", new[] { "TranactionTypeId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "CustomRole_Id1" });
            DropIndex("dbo.AspNetUserRoles", new[] { "CustomRole_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "AspNetUsers_Id1" });
            DropIndex("dbo.AspNetUserRoles", new[] { "AspNetUsers_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "AspNetUsers_Id1" });
            DropIndex("dbo.AspNetUserLogins", new[] { "AspNetUsers_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "AspNetUsers_Id1" });
            DropIndex("dbo.AspNetUserClaims", new[] { "AspNetUsers_Id" });
            DropTable("dbo.WarehousesPlaces");
            DropTable("dbo.TypeOfStorage");
            DropTable("dbo.TranactionType");
            DropTable("dbo.Sizes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Restrictions");
            DropTable("dbo.RestrictionsSet");
            DropTable("dbo.Goods");
            DropTable("dbo.GoodsInWarehauses");
            DropTable("dbo.GoodsInTransaction");
            DropTable("dbo.Transactions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
        }
    }
}
