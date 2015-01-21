using System;
using System.Collections.Generic;
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

        [Required]
        [ForeignKey("Traveller")]
        public virtual int TravellerID { get; set; }
        public virtual Traveller Traveller { get; set; }

        [Required]
        [ForeignKey("Journey")]
        public virtual int JourneyID { get; set; }
        public virtual Journey Journey { get; set; }

        [Required]
        [ForeignKey("Fare")]
        public virtual int FareID { get; set; }
        public virtual Fare Fare { get; set; }

        public Ticket()
        {



        }


    }
}