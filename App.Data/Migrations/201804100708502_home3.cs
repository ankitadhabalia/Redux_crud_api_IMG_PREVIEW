namespace App.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class home3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Image = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductImages");
        }
    }
}
