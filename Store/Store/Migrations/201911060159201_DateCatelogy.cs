namespace Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateCatelogy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Catelogies", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Catelogies", "Date");
        }
    }
}
