namespace SOFT331_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedtickettoworkproperly : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "FareID", "dbo.Fares");
            DropForeignKey("dbo.Tickets", "JourneyID", "dbo.Journeys");
            DropIndex("dbo.Tickets", new[] { "JourneyID" });
            DropIndex("dbo.Tickets", new[] { "FareID" });
            RenameColumn(table: "dbo.Tickets", name: "JourneyID", newName: "Journey_JourneyID");
            AddColumn("dbo.Tickets", "TicketDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tickets", "FareType", c => c.String(nullable: false));
            AddColumn("dbo.Tickets", "EventType", c => c.String(nullable: false));
            AddColumn("dbo.Tickets", "TicketPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Tickets", "Journey_JourneyID", c => c.Int());
            CreateIndex("dbo.Tickets", "Journey_JourneyID");
            AddForeignKey("dbo.Tickets", "Journey_JourneyID", "dbo.Journeys", "JourneyID");
            DropColumn("dbo.Tickets", "FareID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "FareID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tickets", "Journey_JourneyID", "dbo.Journeys");
            DropIndex("dbo.Tickets", new[] { "Journey_JourneyID" });
            AlterColumn("dbo.Tickets", "Journey_JourneyID", c => c.Int(nullable: false));
            DropColumn("dbo.Tickets", "TicketPrice");
            DropColumn("dbo.Tickets", "EventType");
            DropColumn("dbo.Tickets", "FareType");
            DropColumn("dbo.Tickets", "TicketDate");
            RenameColumn(table: "dbo.Tickets", name: "Journey_JourneyID", newName: "JourneyID");
            CreateIndex("dbo.Tickets", "FareID");
            CreateIndex("dbo.Tickets", "JourneyID");
            AddForeignKey("dbo.Tickets", "JourneyID", "dbo.Journeys", "JourneyID", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "FareID", "dbo.Fares", "FareID", cascadeDelete: true);
        }
    }
}
