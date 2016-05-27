namespace EF.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PermissionSet", "PermissionsId", "dbo.Permissions");
            DropForeignKey("dbo.PermissionSet", "RolesId", "dbo.Roles");
            DropForeignKey("dbo.Users", "RolesId", "dbo.Roles");
            DropForeignKey("dbo.Transactions", "UsersId", "dbo.Users");
            DropIndex("dbo.Transactions", new[] { "UsersId" });
            DropIndex("dbo.Users", new[] { "RolesId" });
            DropIndex("dbo.PermissionSet", new[] { "RolesId" });
            DropIndex("dbo.PermissionSet", new[] { "PermissionsId" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.PermissionSet");
            DropTable("dbo.Permissions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Permissions",
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
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.PermissionSet", "PermissionsId");
            CreateIndex("dbo.PermissionSet", "RolesId");
            CreateIndex("dbo.Users", "RolesId");
            CreateIndex("dbo.Transactions", "UsersId");
            AddForeignKey("dbo.Transactions", "UsersId", "dbo.Users", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Users", "RolesId", "dbo.Roles", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PermissionSet", "RolesId", "dbo.Roles", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PermissionSet", "PermissionsId", "dbo.Permissions", "ID", cascadeDelete: true);
        }
    }
}
