namespace App.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class home : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductFiles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        jobTitleName = c.String(),
                        firstName = c.String(),
                        lastName = c.String(),
                        phoneNumber = c.String(),
                        emailAddress = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductFiles");
        }
    }
}
