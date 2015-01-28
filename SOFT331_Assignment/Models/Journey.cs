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

        public Dictionary<String, int> getFareTypesSummary()
        {
            Dictionary<string, int> dic = new Dictionary<string,int>();

            //loop through tickets
            foreach (Ticket t in this.Tickets)
            {
                //if fare type is in list
                if (dic.ContainsKey(t.Fare.FareType.FareTypeDescription))
                {
                    //increment value
                    dic[t.Fare.FareType.FareTypeDescription]++;
                }
                else
                {
                    //add fare type to dictionary
                    dic.Add(t.Fare.FareType.FareTypeDescription, 1);
                }
                    
            }

            return dic;
        }

        public Dictionary<String, int> getTicketGroupsSummary()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();

            //loop through tickets
            foreach (Ticket t in this.Tickets)
            {
                //if ticket group is in list
                if (dic.ContainsKey(t.Fare.TicketType.Name))
                {
                    //increment value
                    dic[t.Fare.TicketType.Name]++;
                }
                else
                {
                    //add ticket group to dictionary
                    dic.Add(t.Fare.TicketType.Name, 1);
                }

            }

            return dic;
        }

        public KeyValuePair<int, double> getGiftAidSummary()
        {
            double totalPrice = 0;
            int numberTickets = 0;

            //loop through tickets
            foreach (Ticket t in this.Tickets)
            {
                if (t.GiftAid)
                {
                    totalPrice = totalPrice + t.Fare.GiftAidPrice;
                    numberTickets++;
                }
            }
            
            return new KeyValuePair<int,double>(numberTickets,totalPrice);
        }

        public DateTime getJourneyDate()
        {
            return ((DateTime)this.Stops.Min(s => s.DepartureTime)).Date;
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