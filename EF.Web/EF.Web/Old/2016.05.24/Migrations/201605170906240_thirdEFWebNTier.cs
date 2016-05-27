namespace EF.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thirdEFWebNTier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Quantity = c.Int(nullable: false),
                        TypeOfStorageId = c.Long(nullable: false),
                        SizesId = c.Long(nullable: false),
                        Info = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sizes", t => t.SizesId, cascadeDelete: true)
                .ForeignKey("dbo.TypeOfStorage", t => t.TypeOfStorageId, cascadeDelete: true)
                .Index(t => t.TypeOfStorageId)
                .Index(t => t.SizesId);
            
            CreateTable(
                "dbo.GoodsInTransaction",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        TransactionsId = c.Long(nullable: false),
                        GoodsId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Goods", t => t.GoodsId, cascadeDelete: true)
                .ForeignKey("dbo.Transactions", t => t.TransactionsId, cascadeDelete: true)
                .Index(t => t.TransactionsId)
                .Index(t => t.GoodsId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        TranactionTypeId = c.Long(nullable: false),
                        UsersId = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TranactionType", t => t.TranactionTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UsersId, cascadeDelete: true)
                .Index(t => t.TranactionTypeId)
                .Index(t => t.UsersId);
            
            CreateTable(
                "dbo.TranactionType",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RolesId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RolesId, cascadeDelete: true)
                .Index(t => t.RolesId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PermissionSet",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        RolesId = c.Long(nullable: false),
                        PermissionsId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Permissions", t => t.PermissionsId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RolesId, cascadeDelete: true)
                .Index(t => t.RolesId)
                .Index(t => t.PermissionsId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GoodsInWarehauses",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        GoodsId = c.Long(nullable: false),
                        WarehousesPlacesId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Goods", t => t.GoodsId, cascadeDelete: true)
                .ForeignKey("dbo.WarehousesPlaces", t => t.WarehousesPlacesId, cascadeDelete: true)
                .Index(t => t.GoodsId)
                .Index(t => t.WarehousesPlacesId);
            
            CreateTable(
                "dbo.WarehousesPlaces",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Adress = c.String(nullable: false),
                        Place = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RestrictionsSet",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        RestrictionsId = c.Long(nullable: false),
                        GoodsId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Goods", t => t.GoodsId, cascadeDelete: true)
                .ForeignKey("dbo.Restrictions", t => t.RestrictionsId, cascadeDelete: true)
                .Index(t => t.RestrictionsId)
                .Index(t => t.GoodsId);
            
            CreateTable(
                "dbo.Restrictions",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        RestrictionName = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Size = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TypeOfStorage",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Goods", "TypeOfStorageId", "dbo.TypeOfStorage");
            DropForeignKey("dbo.Goods", "SizesId", "dbo.Sizes");
            DropForeignKey("dbo.RestrictionsSet", "RestrictionsId", "dbo.Restrictions");
            DropForeignKey("dbo.RestrictionsSet", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.GoodsInWarehauses", "WarehousesPlacesId", "dbo.WarehousesPlaces");
            DropForeignKey("dbo.GoodsInWarehauses", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.Transactions", "UsersId", "dbo.Users");
            DropForeignKey("dbo.Users", "RolesId", "dbo.Roles");
            DropForeignKey("dbo.PermissionSet", "RolesId", "dbo.Roles");
            DropForeignKey("dbo.PermissionSet", "PermissionsId", "dbo.Permissions");
            DropForeignKey("dbo.Transactions", "TranactionTypeId", "dbo.TranactionType");
            DropForeignKey("dbo.GoodsInTransaction", "TransactionsId", "dbo.Transactions");
            DropForeignKey("dbo.GoodsInTransaction", "GoodsId", "dbo.Goods");
            DropIndex("dbo.RestrictionsSet", new[] { "GoodsId" });
            DropIndex("dbo.RestrictionsSet", new[] { "RestrictionsId" });
            DropIndex("dbo.GoodsInWarehauses", new[] { "WarehousesPlacesId" });
            DropIndex("dbo.GoodsInWarehauses", new[] { "GoodsId" });
            DropIndex("dbo.PermissionSet", new[] { "PermissionsId" });
            DropIndex("dbo.PermissionSet", new[] { "RolesId" });
            DropIndex("dbo.Users", new[] { "RolesId" });
            DropIndex("dbo.Transactions", new[] { "UsersId" });
            DropIndex("dbo.Transactions", new[] { "TranactionTypeId" });
            DropIndex("dbo.GoodsInTransaction", new[] { "GoodsId" });
            DropIndex("dbo.GoodsInTransaction", new[] { "TransactionsId" });
            DropIndex("dbo.Goods", new[] { "SizesId" });
            DropIndex("dbo.Goods", new[] { "TypeOfStorageId" });
            DropTable("dbo.TypeOfStorage");
            DropTable("dbo.Sizes");
            DropTable("dbo.Restrictions");
            DropTable("dbo.RestrictionsSet");
            DropTable("dbo.WarehousesPlaces");
            DropTable("dbo.GoodsInWarehauses");
            DropTable("dbo.Permissions");
            DropTable("dbo.PermissionSet");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.TranactionType");
            DropTable("dbo.Transactions");
            DropTable("dbo.GoodsInTransaction");
            DropTable("dbo.Goods");
        }
    }
}
