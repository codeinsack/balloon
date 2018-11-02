namespace Balloon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Delivery", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Delivery", c => c.Boolean(nullable: false));
        }
    }
}
