namespace SOFT331_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventTypes",
                c => new
                    {
                        EventTypeID = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false),
                        EventDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventTypeID);
            
            CreateTable(
                "dbo.Fares",
                c => new
                    {
                        FareID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        FareTypeID = c.Int(nullable: false),
                        EventTypeID = c.Int(nullable: false),
                        BasicPrice = c.Double(nullable: false),
                        GiftAidPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.FareID)
                .ForeignKey("dbo.EventTypes", t => t.EventTypeID, cascadeDelete: true)
                .ForeignKey("dbo.FareTypes", t => t.FareTypeID, cascadeDelete: true)
                .Index(t => t.FareTypeID)
                .Index(t => t.EventTypeID);
            
            CreateTable(
                "dbo.FareTypes",
                c => new
                    {
                        FaretypeID = c.Int(nullable: false, identity: true),
                        FareTypeDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FaretypeID);
            
            CreateTable(
                "dbo.Journeys",
                c => new
                    {
                        JourneyID = c.Int(nullable: false, identity: true),
                        TrainID = c.Int(nullable: false),
                        DepartureStationID = c.Int(nullable: false),
                        ArrivalStationID = c.Int(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        ArrivalTime = c.DateTime(nullable: false),
                        JourneyTypeID = c.Int(nullable: false),
                        AdvanceTickets = c.Int(nullable: false),
                        NumberOfSeats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JourneyID)
                .ForeignKey("dbo.Stations", t => t.ArrivalStationID)
                .ForeignKey("dbo.Stations", t => t.DepartureStationID)
                .ForeignKey("dbo.JourneyTypes", t => t.JourneyTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Trains", t => t.TrainID, cascadeDelete: true)
                .Index(t => t.TrainID)
                .Index(t => t.DepartureStationID)
                .Index(t => t.ArrivalStationID)
                .Index(t => t.JourneyTypeID);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        StationID = c.Int(nullable: false, identity: true),
                        StationName = c.String(nullable: false),
                        StationDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StationID);
            
            CreateTable(
                "dbo.JourneyTypes",
                c => new
                    {
                        JourneyTypeID = c.Int(nullable: false, identity: true),
                        JourneyTypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.JourneyTypeID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        TravellerID = c.Int(nullable: false),
                        JourneyID = c.Int(nullable: false),
                        FareID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.Fares", t => t.FareID, cascadeDelete: true)
                .ForeignKey("dbo.Journeys", t => t.JourneyID, cascadeDelete: true)
                .ForeignKey("dbo.Travellers", t => t.TravellerID, cascadeDelete: true)
                .Index(t => t.TravellerID)
                .Index(t => t.JourneyID)
                .Index(t => t.FareID);
            
            CreateTable(
                "dbo.Travellers",
                c => new
                    {
                        TravellerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address1 = c.String(),
                        PostCode = c.String(),
                    })
                .PrimaryKey(t => t.TravellerID);
            
            CreateTable(
                "dbo.Trains",
                c => new
                    {
                        TrainID = c.Int(nullable: false, identity: true),
                        TrainNumber = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Image = c.String(),
                        Maker = c.String(),
                        Year = c.String(nullable: false),
                        WorksNumber = c.Int(nullable: false),
                        Type = c.String(),
                        DrivingWheelDiameter = c.Double(nullable: false),
                        TrailingWheelDiameter = c.Double(nullable: false),
                        TotalWheelbase = c.Double(nullable: false),
                        CylinderSize = c.Double(nullable: false),
                        HeatingSurface = c.Double(nullable: false),
                        WorkingPressure = c.Double(nullable: false),
                        TractiveEffort = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        LengthOverBuffers = c.Double(nullable: false),
                        DonorLoco = c.String(),
                    })
                .PrimaryKey(t => t.TrainID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Journeys", "TrainID", "dbo.Trains");
            DropForeignKey("dbo.Tickets", "TravellerID", "dbo.Travellers");
            DropForeignKey("dbo.Tickets", "JourneyID", "dbo.Journeys");
            DropForeignKey("dbo.Tickets", "FareID", "dbo.Fares");
            DropForeignKey("dbo.Journeys", "JourneyTypeID", "dbo.JourneyTypes");
            DropForeignKey("dbo.Journeys", "DepartureStationID", "dbo.Stations");
            DropForeignKey("dbo.Journeys", "ArrivalStationID", "dbo.Stations");
            DropForeignKey("dbo.Fares", "FareTypeID", "dbo.FareTypes");
            DropForeignKey("dbo.Fares", "EventTypeID", "dbo.EventTypes");
            DropIndex("dbo.Tickets", new[] { "FareID" });
            DropIndex("dbo.Tickets", new[] { "JourneyID" });
            DropIndex("dbo.Tickets", new[] { "TravellerID" });
            DropIndex("dbo.Journeys", new[] { "JourneyTypeID" });
            DropIndex("dbo.Journeys", new[] { "ArrivalStationID" });
            DropIndex("dbo.Journeys", new[] { "DepartureStationID" });
            DropIndex("dbo.Journeys", new[] { "TrainID" });
            DropIndex("dbo.Fares", new[] { "EventTypeID" });
            DropIndex("dbo.Fares", new[] { "FareTypeID" });
            DropTable("dbo.Trains");
            DropTable("dbo.Travellers");
            DropTable("dbo.Tickets");
            DropTable("dbo.JourneyTypes");
            DropTable("dbo.Stations");
            DropTable("dbo.Journeys");
            DropTable("dbo.FareTypes");
            DropTable("dbo.Fares");
            DropTable("dbo.EventTypes");
        }
    }
}
