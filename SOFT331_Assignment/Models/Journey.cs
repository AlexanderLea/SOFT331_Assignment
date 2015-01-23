using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class Journey
    {
        //+ JourneyID: int
        //+ LocomotiveID: int
        //+ Locomotive: Locomotive
        //+ EventID: int
        //+ Event: Event
        //+ NoSeats: int
        //+ AdvanceTickets: int
        //+ FirstClassTickets: int
        //+ Stops: List<Stop>

        [Key]
        public int JourneyID { get; set; }

        [Required]
        [ForeignKey("Train")]
        public virtual int TrainID { get; set; }
        public virtual Train Train { get; set; }

        [Required]
        [ForeignKey("Event")]
        public virtual int EventID { get; set; }
        public virtual Event Event { get; set; }

        [Required]
        [DisplayName("Advance Tickets (defaults to 150)")]
        public int AdvanceTickets { get; set; }

        [Required]
        [DisplayName("First Class Tickets")]
        public int FirstClassTickets { get; set; }

        [DisplayName("# Seats")]
        public int NumberOfSeats { get; set; }

        public virtual ICollection<Stop> Stops { get; set; }

        public Journey()
        {
            NumberOfSeats = 150;
        }


//        + GetAvailableSeats(): int
//+ GetAvailableSeatsBetween(_depart: Station, _arrive: Station): int
//+ CanBookWheelchair(_depart: Station, _arrive: Station): bool
//+ BookTickets(_depart: Station, _arrive: Station, _noTickets: int:): bool

        public int getAvailableSeats()
        {
            throw new NotImplementedException();
        }

        public int getAvailableSeatsBetween(Station _depart, Station _arrive)
        {
            throw new NotImplementedException();
        }

        public bool canBookWheelchair(Station _depart, Station _arrive)
        {
            throw new NotImplementedException();
        }

        public bool bookTickets(Station _depart, Station _arrive, int _noTickets)
        {
            throw new NotImplementedException();
        }

        public void allocateAdvanceTickets(int _noTickets)
        {
            if (NumberOfSeats - (AdvanceTickets + _noTickets) > 0)
            {
                AdvanceTickets = +_noTickets;
            }
        }
    }
}