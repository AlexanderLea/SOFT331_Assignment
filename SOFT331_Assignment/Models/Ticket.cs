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
        //+ TicketID: int
        //+ CustomerID: int
        //+ Customer: Customer
        //+ FareID: int
        //+ Fare: Fare
        //+ GiftAid: bool
        //+ Wheelchair: bool
        //+ Carer: bool
        //+ Book(): bool

        [Key]
        public int TicketID { get; set; }

        [Required]
        [ForeignKey("Traveller")]
        public virtual int? TravellerID { get; set; }
        public virtual Traveller Traveller { get; set; }

        [Required]
        [DisplayName("Fare")]
        public virtual int FareID { get; set; }
        public virtual Fare Fare { get; set; }

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

        public bool book()
        {
            throw new NotImplementedException();
        }


    }
}