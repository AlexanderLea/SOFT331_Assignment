using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Fare> Fares { get; set; }
        public DbSet<FareType> FareTypes { get; set; }
        public DbSet<Journey> Journies { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Traveller> Travellers { get; set; }
        
    }
}