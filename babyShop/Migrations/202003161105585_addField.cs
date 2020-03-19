namespace babyShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Categories", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Categories", "CreateBy", c => c.String());
            AddColumn("dbo.Categories", "UpdateBy", c => c.String());
            AddColumn("dbo.Products", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "UpdateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "CreateBy", c => c.String());
            AddColumn("dbo.Products", "UpdateBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "UpdateBy");
            DropColumn("dbo.Products", "CreateBy");
            DropColumn("dbo.Products", "UpdateDate");
            DropColumn("dbo.Products", "CreateDate");
            DropColumn("dbo.Categories", "UpdateBy");
            DropColumn("dbo.Categories", "CreateBy");
            DropColumn("dbo.Categories", "UpdateDate");
            DropColumn("dbo.Categories", "CreateDate");
        }
    }
}
