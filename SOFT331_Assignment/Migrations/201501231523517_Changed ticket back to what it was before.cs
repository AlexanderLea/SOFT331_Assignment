namespace SOFT331_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedticketbacktowhatitwasbefore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "FareID", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "GiftAid", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Tickets", "FareID");
            AddForeignKey("dbo.Tickets", "FareID", "dbo.Fares", "FareID", cascadeDelete: true);
            DropColumn("dbo.Tickets", "FareType");
            DropColumn("dbo.Tickets", "EventType");
            DropColumn("dbo.Tickets", "TicketPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "TicketPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Tickets", "EventType", c => c.String(nullable: false));
            AddColumn("dbo.Tickets", "FareType", c => c.String(nullable: false));
            DropForeignKey("dbo.Tickets", "FareID", "dbo.Fares");
            DropIndex("dbo.Tickets", new[] { "FareID" });
            DropColumn("dbo.Tickets", "GiftAid");
            DropColumn("dbo.Tickets", "FareID");
        }
    }
}
