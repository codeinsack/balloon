namespace Balloon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "SellingSuccess", c => c.Double(nullable: false));
            DropColumn("dbo.Goods", "SellingSucces");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goods", "SellingSucces", c => c.Double(nullable: false));
            DropColumn("dbo.Goods", "SellingSuccess");
        }
    }
}
