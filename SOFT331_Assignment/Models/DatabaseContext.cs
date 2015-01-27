using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class DatabaseContext : DbContext
    {
        
        public DbSet<Fare> Fares { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<FareType> FareTypes { get; set; }
        public DbSet<Journey> Journies { get; set; }        
        public DbSet<Station> Stations { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketGroup> TicketTypes { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Traveller> Travellers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketGroup>()
                    .HasRequired(m => m.ArrivalStation)
                    .WithMany(t => t.arrivalTicketTypes)
                    .HasForeignKey(m => m.ArrivalStationID)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<TicketGroup>()
                    .HasRequired(m => m.DepartureStation)
                    .WithMany(t => t.departureTicketTypes)
                    .HasForeignKey(m => m.DepartureStationID)
                    .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}