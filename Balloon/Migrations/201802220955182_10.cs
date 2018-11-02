namespace Balloon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "SellingSuccess", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "SellingSuccess");
        }
    }
}
