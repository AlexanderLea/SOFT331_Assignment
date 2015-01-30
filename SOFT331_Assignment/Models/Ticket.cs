using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        
        [ForeignKey("Traveller")]
        public int? TravellerID { get; set; }
        public virtual Traveller Traveller { get; set; }

        [Required]
        [DisplayName("Fare")]
        public int FareID { get; set; }
        public virtual Fare Fare { get; set; }

        [Required]
        [DisplayName("Journey")]
        public virtual int JourneyID { get; set; }
        public virtual Journey Journey { get; set; }

        [Required]
        [DisplayName("Gift Aid")]
        public bool GiftAid { get; set; }

        [Required]
        [DisplayName("Wheelchair")]
        public bool Wheelchair { get; set; }

        [Required]
        [DisplayName("Carer")]
        public bool Carer { get; set; }

        public Ticket()
        {
            this.Wheelchair = false;
            this.Carer = false;
        }

        /// <summary>
        /// Deletes all associated journeys and sub-classes of journey when this ticket is deleted
        /// </summary>
        public void delete()
        {
            this.Journey.deleteTicket(this.Fare.TicketType.DepartureStation, this.Fare.TicketType.ArrivalStation, this.Wheelchair);
        }

        /// <summary>
        /// Books this ticket, and saves values against stops
        /// </summary>
        /// <returns>Boolean successful save</returns>
        public bool book()
        {
            bool success = false;

            if(this.Wheelchair)
            {
                if (this.Journey.canBookWheelchair(this.Fare.TicketType.DepartureStation, this.Fare.TicketType.ArrivalStation))
                {
                    success = this.Journey.bookTickets(this.Fare.TicketType.DepartureStation, this.Fare.TicketType.ArrivalStation, this.Wheelchair);
                }
            }
            else
            {
                success = this.Journey.bookTickets(this.Fare.TicketType.DepartureStation, this.Fare.TicketType.ArrivalStation, this.Wheelchair);
            }
            return success;
        }

        
    }
}