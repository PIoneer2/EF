namespace EF.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondEFWebNTier : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Info = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GoodsInTransaction", "TransactionsId", "dbo.Transactions");
            DropForeignKey("dbo.Transactions", "UsersId", "dbo.Users");
            DropForeignKey("dbo.Users", "RolesId", "dbo.Roles");
            DropForeignKey("dbo.PermissionSet", "RolesId", "dbo.Roles");
            DropForeignKey("dbo.PermissionSet", "PermissionsId", "dbo.Permissions");
            DropForeignKey("dbo.Transactions", "TranactionTypeId", "dbo.TranactionType");
            DropForeignKey("dbo.GoodsInTransaction", "GoodsId", "dbo.Goods");
            DropIndex("dbo.PermissionSet", new[] { "PermissionsId" });
            DropIndex("dbo.PermissionSet", new[] { "RolesId" });
            DropIndex("dbo.Users", new[] { "RolesId" });
            DropIndex("dbo.Transactions", new[] { "UsersId" });
            DropIndex("dbo.Transactions", new[] { "TranactionTypeId" });
            DropIndex("dbo.GoodsInTransaction", new[] { "GoodsId" });
            DropIndex("dbo.GoodsInTransaction", new[] { "TransactionsId" });
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
