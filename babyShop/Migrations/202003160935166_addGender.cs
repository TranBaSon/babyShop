namespace babyShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ForGender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "ForGender");
        }
    }
}
