namespace RPGSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Galleries", "Picture", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Galleries", "Picture", c => c.Binary(maxLength: 4096));
        }
    }
}
