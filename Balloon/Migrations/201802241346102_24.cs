namespace Balloon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _24 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Delivery", c => c.Boolean());
            AddColumn("dbo.Orders", "DeliveryDate", c => c.DateTime());
            AddColumn("dbo.Orders", "DeliveryAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DeliveryAddress");
            DropColumn("dbo.Orders", "DeliveryDate");
            DropColumn("dbo.Orders", "Delivery");
        }
    }
}
