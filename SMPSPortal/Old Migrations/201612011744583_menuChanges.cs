namespace SmpsPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class menuChanges : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MenuComponents", name: "MenuId", newName: "Menu_Id");
            RenameIndex(table: "dbo.MenuComponents", name: "IX_MenuId", newName: "IX_Menu_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MenuComponents", name: "IX_Menu_Id", newName: "IX_MenuId");
            RenameColumn(table: "dbo.MenuComponents", name: "Menu_Id", newName: "MenuId");
        }
    }
}
