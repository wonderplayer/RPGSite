namespace RPGSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GalleryTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Galleries", "Title", c => c.String(maxLength: 50));
            DropColumn("dbo.Galleries", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Galleries", "Description", c => c.String(maxLength: 100));
            DropColumn("dbo.Galleries", "Title");
        }
    }
}
