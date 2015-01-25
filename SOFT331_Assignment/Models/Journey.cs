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

        [DisplayName("Advance Tickets (defaults to 100)")]
        public int AdvanceTickets { get; set; }

        [Required]
        [DisplayName("First Class Tickets")]
        public int FirstClassTickets { get; set; }

        [DisplayName("# Seats (defaults to 150)")]
        public int NumberOfSeats { get; set; }

        public virtual ICollection<Stop> Stops { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public Journey()
        {
            NumberOfSeats = 150;

            AdvanceTickets = 100;
        }

        public override string ToString()
        {
            string x = Event.Name + ", on " + Train.Name;

            return x;
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
            int minSeats = 0;

            if (AdvanceTickets > 0)
            {
                minSeats = this.AdvanceTickets;

                //get ordered list of stops between _depart and _arrive
                List<Stop> stopsBetween = getStopsBetween(_depart, _arrive);

                foreach (var s in stopsBetween)
                {
                    minSeats = returnSmallestNumber(s.NoOnwardSeats - s.NoBookedSeats, minSeats);
                }
            }

            return minSeats;
        }

        private List<Stop> getStopsBetween(Station _depart, Station _arrive)
        {
            bool go = false;
            List<Stop> temp = new List<Stop>();
            //Order stops by arrival time (null first?), to ensure that we are checking in order
            List<Stop> orderedStops = this.Stops.OrderBy(s => s.ArrivalTime).ToList();

            foreach (Stop s in orderedStops)
            {
                //if stop is departure (i.e. could be first stop, when arrival is null)
                if (s.Station == _depart)
                    go = true;

                if (go)
                    temp.Add(s);

                if (s.Station == _arrive)
                {
                    go = false;
                    break;
                }
            }

            return temp;
        }

        /// <summary>
        /// Checks if there's already a wheelchair booked between the two specified stations
        /// </summary>
        /// <param name="_depart">Departure station for overall ticket-journey </param>
        /// <param name="_arrive">Arrival station for overall ticket-journey</param>
        /// <returns>TRUE/FALSE based on whether a wheelchair ticket can be booked</returns>
        public bool canBookWheelchair(Station _depart, Station _arrive)
        {
            bool wheelchairAvailable = true;

            //Order stops by arrival time (null first?), to ensure that we are checking in order
            List<Stop> orderedStops = getStopsBetween(_depart, _arrive);

            //Loop through stops
            foreach (Stop s in orderedStops)
            {
                //only need one station to not have wheelchair available
                if (s.WheelchairBooked)
                {
                    wheelchairAvailable = false;
                    break;
                }
            }

            return wheelchairAvailable;
        }

        /// <summary>
        /// Book tickets
        /// </summary>
        /// <param name="_depart">Departure Station for overall ticket-journey</param>
        /// <param name="_arrive">Arrival Station for overall ticket-journey</param>
        /// <param name="_noTickets">Number of tickets being booked</param>
        /// <returns>TRUE/FALSE representing transaction success/failure</returns>
        public bool bookTickets(Station _depart, Station _arrive, bool _wheelchair)
        {
            bool success = false;

            if (this.AdvanceTickets - this.Tickets.Count > 0)
            {
                //Order stops by arrival time (null first?), to ensure that we are checking in order
                List<Stop> orderedStops = getStopsBetween(_depart, _arrive);

                //Loop through stops
                foreach (Stop s in orderedStops)
                {
                    success = s.bookTicket(_wheelchair);
                }
            }
            return success;
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