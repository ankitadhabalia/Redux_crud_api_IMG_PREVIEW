namespace App.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class home4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductImages", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductImages", "Image", c => c.Byte(nullable: false));
        }
    }
}
