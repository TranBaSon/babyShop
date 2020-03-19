namespace babyShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeGenser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "ForGender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "ForGender", c => c.Int(nullable: false));
        }
    }
}
