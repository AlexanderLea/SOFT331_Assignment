namespace SOFT331_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedtravellerrequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "TravellerID", "dbo.Travellers");
            DropIndex("dbo.Tickets", new[] { "TravellerID" });
            AlterColumn("dbo.Tickets", "TravellerID", c => c.Int());
            CreateIndex("dbo.Tickets", "TravellerID");
            AddForeignKey("dbo.Tickets", "TravellerID", "dbo.Travellers", "TravellerID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TravellerID", "dbo.Travellers");
            DropIndex("dbo.Tickets", new[] { "TravellerID" });
            AlterColumn("dbo.Tickets", "TravellerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "TravellerID");
            AddForeignKey("dbo.Tickets", "TravellerID", "dbo.Travellers", "TravellerID", cascadeDelete: true);
        }
    }
}
