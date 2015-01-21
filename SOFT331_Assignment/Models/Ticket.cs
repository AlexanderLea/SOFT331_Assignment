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
        //ticketID
        //traveller
        //Date
        //Fare Type
        //Event Type
        //Price paid
        //
        [Key]
        public int TicketID { get; set; }

        [Required]
        [ForeignKey("Traveller")]
        public virtual int TravellerID { get; set; }
        public virtual Traveller Traveller { get; set; }

        [Required]
        [DisplayName("Date of Travel")]
        [DataType(DataType.Date)]
        public DateTime TicketDate { get; set; }

        [Required]
        [DisplayName("Fare Type")]
        public string FareType { get; set; }

        [Required]
        [DisplayName("Ticket Type")]
        public string EventType { get; set; }

        [Required]
        [DisplayName("Ticket Price")]
        [Range(0.00, 100.00, ErrorMessage="Ticket price must be greater than 0, and less than 100")]
        public double TicketPrice { get; set; }

        public Ticket()
        {



        }


    }
}