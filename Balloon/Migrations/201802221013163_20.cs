namespace Balloon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "SellingSucces", c => c.Double(nullable: false));
            DropColumn("dbo.Goods", "SellingSuccess");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goods", "SellingSuccess", c => c.Double(nullable: false));
            DropColumn("dbo.Goods", "SellingSucces");
        }
    }
}
