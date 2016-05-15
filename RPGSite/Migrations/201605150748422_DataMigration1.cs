namespace RPGSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataMigration1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Updated", c => c.DateTime());
            AlterColumn("dbo.Events", "Updated", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Updated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Posts", "Updated", c => c.DateTime(nullable: false));
        }
    }
}
