namespace SOFT331_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removejourneyfromticket : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "JourneyID", "dbo.Journeys");
            DropIndex("dbo.Tickets", new[] { "JourneyID" });
            DropColumn("dbo.Tickets", "JourneyID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "JourneyID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "JourneyID");
            AddForeignKey("dbo.Tickets", "JourneyID", "dbo.Journeys", "JourneyID", cascadeDelete: true);
        }
    }
}
