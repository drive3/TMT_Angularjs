namespace Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CodeProduct", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CodeProduct");
        }
    }
}
