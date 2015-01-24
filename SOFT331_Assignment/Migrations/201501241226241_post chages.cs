namespace SOFT331_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postchages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventID);
            
            CreateTable(
                "dbo.Fares",
                c => new
                    {
                        FareID = c.Int(nullable: false, identity: true),
                        FareTypeID = c.Int(nullable: false),
                        TicketTypeID = c.Int(nullable: false),
                        BasicPrice = c.Double(nullable: false),
                        GiftAidPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.FareID)
                .ForeignKey("dbo.FareTypes", t => t.FareTypeID, cascadeDelete: true)
                .ForeignKey("dbo.TicketTypes", t => t.TicketTypeID, cascadeDelete: true)
                .Index(t => t.FareTypeID)
                .Index(t => t.TicketTypeID);
            
            CreateTable(
                "dbo.FareTypes",
                c => new
                    {
                        FaretypeID = c.Int(nullable: false, identity: true),
                        FareTypeDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FaretypeID);
            
            CreateTable(
                "dbo.TicketTypes",
                c => new
                    {
                        TicketTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DepartureStationID = c.Int(nullable: false),
                        ArrivalStationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TicketTypeID)
                .ForeignKey("dbo.Stations", t => t.ArrivalStationID)
                .ForeignKey("dbo.Stations", t => t.DepartureStationID)
                .Index(t => t.DepartureStationID)
                .Index(t => t.ArrivalStationID);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        StationID = c.Int(nullable: false, identity: true),
                        StationName = c.String(nullable: false),
                        StationDescription = c.String(nullable: false),
                        RequestStop = c.Boolean(nullable: false),
                        WheelchairAccessible = c.Boolean(nullable: false),
                        CarPark = c.Boolean(nullable: false),
                        RefreshmentsAvailable = c.Boolean(nullable: false),
                        ToiletAvailable = c.Boolean(nullable: false),
                        MainlineStationNear = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StationID);
            
            CreateTable(
                "dbo.Journeys",
                c => new
                    {
                        JourneyID = c.Int(nullable: false, identity: true),
                        TrainID = c.Int(nullable: false),
                        EventID = c.Int(nullable: false),
                        AdvanceTickets = c.Int(nullable: false),
                        FirstClassTickets = c.Int(nullable: false),
                        NumberOfSeats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JourneyID)
                .ForeignKey("dbo.Events", t => t.EventID, cascadeDelete: true)
                .ForeignKey("dbo.Trains", t => t.TrainID, cascadeDelete: true)
                .Index(t => t.TrainID)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.Stops",
                c => new
                    {
                        StopID = c.Int(nullable: false, identity: true),
                        StationID = c.Int(nullable: false),
                        JourneyID = c.Int(nullable: false),
                        NoOnwardSeats = c.Int(nullable: false),
                        ArrivalTime = c.DateTime(),
                        DepartureTime = c.DateTime(),
                        WheelchairBooked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StopID)
                .ForeignKey("dbo.Journeys", t => t.JourneyID, cascadeDelete: true)
                .ForeignKey("dbo.Stations", t => t.StationID, cascadeDelete: true)
                .Index(t => t.StationID)
                .Index(t => t.JourneyID);
            
            CreateTable(
                "dbo.Trains",
                c => new
                    {
                        TrainID = c.Int(nullable: false, identity: true),
                        TrainNumber = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Maker = c.String(),
                        Year = c.String(nullable: false),
                        WorksNumber = c.Int(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.TrainID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        TravellerID = c.Int(nullable: false),
                        FareID = c.Int(nullable: false),
                        JourneyID = c.Int(nullable: false),
                        GiftAid = c.Boolean(nullable: false),
                        Wheelchair = c.Boolean(nullable: false),
                        Carer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.Fares", t => t.FareID, cascadeDelete: true)
                .ForeignKey("dbo.Journeys", t => t.JourneyID, cascadeDelete: true)
                .ForeignKey("dbo.Travellers", t => t.TravellerID, cascadeDelete: true)
                .Index(t => t.TravellerID)
                .Index(t => t.FareID)
                .Index(t => t.JourneyID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TravellerID", "dbo.Travellers");
            DropForeignKey("dbo.Tickets", "JourneyID", "dbo.Journeys");
            DropForeignKey("dbo.Tickets", "FareID", "dbo.Fares");
            DropForeignKey("dbo.Journeys", "TrainID", "dbo.Trains");
            DropForeignKey("dbo.Stops", "StationID", "dbo.Stations");
            DropForeignKey("dbo.Stops", "JourneyID", "dbo.Journeys");
            DropForeignKey("dbo.Journeys", "EventID", "dbo.Events");
            DropForeignKey("dbo.Fares", "TicketTypeID", "dbo.TicketTypes");
            DropForeignKey("dbo.TicketTypes", "DepartureStationID", "dbo.Stations");
            DropForeignKey("dbo.TicketTypes", "ArrivalStationID", "dbo.Stations");
            DropForeignKey("dbo.Fares", "FareTypeID", "dbo.FareTypes");
            DropIndex("dbo.Tickets", new[] { "JourneyID" });
            DropIndex("dbo.Tickets", new[] { "FareID" });
            DropIndex("dbo.Tickets", new[] { "TravellerID" });
            DropIndex("dbo.Stops", new[] { "JourneyID" });
            DropIndex("dbo.Stops", new[] { "StationID" });
            DropIndex("dbo.Journeys", new[] { "EventID" });
            DropIndex("dbo.Journeys", new[] { "TrainID" });
            DropIndex("dbo.TicketTypes", new[] { "ArrivalStationID" });
            DropIndex("dbo.TicketTypes", new[] { "DepartureStationID" });
            DropIndex("dbo.Fares", new[] { "TicketTypeID" });
            DropIndex("dbo.Fares", new[] { "FareTypeID" });
            DropTable("dbo.Travellers");
            DropTable("dbo.Tickets");
            DropTable("dbo.Trains");
            DropTable("dbo.Stops");
            DropTable("dbo.Journeys");
            DropTable("dbo.Stations");
            DropTable("dbo.TicketTypes");
            DropTable("dbo.FareTypes");
            DropTable("dbo.Fares");
            DropTable("dbo.Events");
        }
    }
}
