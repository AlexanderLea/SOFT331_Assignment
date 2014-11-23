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
        public virtual int DepartureStationID { get; set; }
        public virtual Station DepartureStation { get; set; }

        [Required]
        public virtual int ArrivalStationID { get; set; }
        public virtual Station ArrivalStation { get; set; }


        [DisplayName("Departure Time")]
        [Required]
        public DateTime DepartureTime { get; set; }

        [DisplayName("Arrival Time")]
        [Required]
        public DateTime ArrivalTime { get; set; }

        [DisplayName("Journey Type")]
        [Required]
        public string JourneyType { get; set; } //E.g. "Christmas", "Super special awesome journey" etc

        [Required]
        [DisplayName("Advance Tickets")]
        public int AdvanceTickets { get; set; }

        [DisplayName("# Seats")]
        public int NumberOfSeats { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public Journey()
        {
            NumberOfSeats = 150;
        }

        public void allocateAdvanceTickets(int _noTickets)
        {
            if (NumberOfSeats - (AdvanceTickets + _noTickets) > 0)
            {
                AdvanceTickets = +_noTickets;
            }
        }

        public bool areTicketsAvailable(int _quantity)
        {
            if ((this.AdvanceTickets - _quantity) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool bookTickets(int _quantity)
        {
            if (areTicketsAvailable(_quantity))
            {
                AdvanceTickets = AdvanceTickets - _quantity;
                return true;
            }
            else return false;
        }

        public bool canBookDisabled()
        {
           // DatabaseContext db = new DatabaseContext();

            throw new NotImplementedException();

            return true;
        }
    }
}