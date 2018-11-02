namespace Balloon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Delivery");
            DropColumn("dbo.Orders", "DeliveryDate");
            DropColumn("dbo.Orders", "DeliveryAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "DeliveryAddress", c => c.String());
            AddColumn("dbo.Orders", "DeliveryDate", c => c.DateTime());
            AddColumn("dbo.Orders", "Delivery", c => c.Boolean());
        }
    }
}
