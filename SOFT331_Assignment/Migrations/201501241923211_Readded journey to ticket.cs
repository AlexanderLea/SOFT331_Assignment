namespace SOFT331_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Readdedjourneytoticket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stops", "NoBookedSeats", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "JourneyID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "JourneyID");
            AddForeignKey("dbo.Tickets", "JourneyID", "dbo.Journeys", "JourneyID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "JourneyID", "dbo.Journeys");
            DropIndex("dbo.Tickets", new[] { "JourneyID" });
            DropColumn("dbo.Tickets", "JourneyID");
            DropColumn("dbo.Stops", "NoBookedSeats");
        }
    }
}
