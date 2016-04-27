namespace RPGSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaxLengthToEquipmentImage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Equipments", "Picture", c => c.Binary(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Equipments", "Picture", c => c.Binary(nullable: false));
        }
    }
}
