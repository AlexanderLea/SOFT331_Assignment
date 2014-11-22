using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class Fare
    {
        [Key]
        public int FareID { get; set; }

        [Required]
        public string Description { get; set; }

        //[ScaffoldColumn(false)]
        
        public virtual int FareTypeID { get; set; }
        [DisplayName("Fare Type")]
        [ForeignKey("FareTypeID")]
        public virtual FareType Type { get; set; }

        public virtual int TicketTypeID { get; set; }
        [DisplayName("Ticket Type")]
        [ForeignKey("TicketTypeID")]
        public virtual EventType TypeOfTicket { get; set; }

        [Required(ErrorMessage = "Fare must have a basic price")]
        public double BasicPrice { get; set; }
        public double GiftAidPrice { get; set; }

        public Fare()
        {
            if (GiftAidPrice == 0)
            {
                GiftAidPrice = BasicPrice;
            }
        }
    }
}