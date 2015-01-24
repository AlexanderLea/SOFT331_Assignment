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

        public override string ToString()
        {
            string x = Event.Name + ", on " + Train.Name;

            return x;
        }

        //        + GetAvailableSeats(): int
        //+ GetAvailableSeatsBetween(_depart: Station, _arrive: Station): int
        //+ CanBookWheelchair(_depart: Station, _arrive: Station): bool
        //+ BookTickets(_depart: Station, _arrive: Station, _noTickets: int:): bool

        /// <summary>
        /// Gets the maximum number of seats that can be booked for a complete journey
        /// </summary>
        /// <returns>maximum # seats for whole journey</returns>
        public int getAvailableSeats()
        {
            int minSeats = this.NumberOfSeats;

            foreach (Stop s in this.Stops)
            {
                minSeats = returnSmallestNumber(s.NoOnwardSeats, minSeats);
            }

            return minSeats;
        }

        /// <summary>
        /// Gets the number of bookable seats between the departure and arrival stations
        /// specified
        /// </summary>
        /// <param name="_depart">Station where the ticket "journey" should begin</param>
        /// <param name="_arrive">Station where the ticket "journey" should end</param>
        /// <returns>Number of available seats</returns>
        public int getAvailableSeatsBetween(Station _depart, Station _arrive)
        {
            int minSeats = this.NumberOfSeats;
            
            //Order stops by arrival time (null first?), to ensure that we are checking in order
            List<Stop> orderedStops = this.Stops.OrderBy(s => s.ArrivalTime).ToList();

            //Loop through stops
            foreach (Stop s in orderedStops)
            {
                //if stop is departure (i.e. could be first stop, when arrival is null)
                if (s.Station == _depart)
                {                    
                    while (s.Station != _arrive)
                    {
                        minSeats = returnSmallestNumber(s.NoOnwardSeats, minSeats);
                    }
                }
            }

            return minSeats;
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

        private int returnSmallestNumber(int _a, int _b)
        {
            if (_a < _b)
                return _a;
            else
                return _b;
        }
    }
}