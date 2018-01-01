namespace SmpsPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iconTomenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuComponents", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuComponents", "Icon");
        }
    }
}
