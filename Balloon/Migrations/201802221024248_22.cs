namespace Balloon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Goods", "SellingSuccess", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Goods", "SellingSuccess", c => c.Double(nullable: false));
        }
    }
}
