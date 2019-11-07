namespace Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionCatelogy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catelogies", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catelogies", "Description");
        }
    }
}
