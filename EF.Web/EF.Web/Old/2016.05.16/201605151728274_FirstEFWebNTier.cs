namespace EF.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstEFWebNTier : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Roles",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "UsersId", "dbo.Users");
            DropForeignKey("dbo.Transactions", "TranactionTypeId", "dbo.TranactionType");
            DropForeignKey("dbo.Users", "RolesId", "dbo.Roles");
            DropForeignKey("dbo.PermissionSet", "RolesId", "dbo.Roles");
            DropForeignKey("dbo.PermissionSet", "PermissionsId", "dbo.Permissions");
            DropIndex("dbo.Transactions", new[] { "UsersId" });
            DropIndex("dbo.Transactions", new[] { "TranactionTypeId" });
            DropIndex("dbo.Users", new[] { "RolesId" });
            DropIndex("dbo.PermissionSet", new[] { "PermissionsId" });
            DropIndex("dbo.PermissionSet", new[] { "RolesId" });
            DropTable("dbo.TranactionType");
            DropTable("dbo.Transactions");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Permissions");
            DropTable("dbo.PermissionSet");
        }
    }
}
