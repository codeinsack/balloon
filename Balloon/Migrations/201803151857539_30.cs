namespace Balloon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _30 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Delivery", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Orders", "DeliveryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "DeliveryDate", c => c.DateTime());
            AlterColumn("dbo.Orders", "Delivery", c => c.Boolean());
        }
    }
}
