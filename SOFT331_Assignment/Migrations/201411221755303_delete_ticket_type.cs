namespace SOFT331_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_ticket_type : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fares", "FareTypeID", "dbo.FareTypes");
            DropForeignKey("dbo.Fares", "TicketTypeID", "dbo.EventTypes");
            DropIndex("dbo.Fares", new[] { "FareTypeID" });
            DropIndex("dbo.Fares", new[] { "TicketTypeID" });
            AddColumn("dbo.Fares", "EventTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Fares", "EventTypeID");
            CreateIndex("dbo.Fares", "FareTypeID");
            AddForeignKey("dbo.Fares", "EventTypeID", "dbo.EventTypes", "EventTypeID", cascadeDelete: true);
            AddForeignKey("dbo.Fares", "FareTypeID", "dbo.FareTypes", "FaretypeID", cascadeDelete: true);
            DropColumn("dbo.Fares", "TicketTypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fares", "TicketTypeID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Fares", "FareTypeID", "dbo.FareTypes");
            DropForeignKey("dbo.Fares", "EventTypeID", "dbo.EventTypes");
            DropIndex("dbo.Fares", new[] { "FareTypeID" });
            DropIndex("dbo.Fares", new[] { "EventTypeID" });
            DropColumn("dbo.Fares", "EventTypeID");
            CreateIndex("dbo.Fares", "TicketTypeID");
            CreateIndex("dbo.Fares", "FareTypeID");
            AddForeignKey("dbo.Fares", "TicketTypeID", "dbo.EventTypes", "EventTypeID", cascadeDelete: true);
            AddForeignKey("dbo.Fares", "FareTypeID", "dbo.FareTypes", "FaretypeID", cascadeDelete: true);
        }
    }
}
