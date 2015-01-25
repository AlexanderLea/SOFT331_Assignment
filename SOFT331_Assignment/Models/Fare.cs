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
        //+ FareID: int
        //+ FareTypeID: int
        //+ FareType: FareType
        //+ TicketTypeID: int
        //+ TicketType: TicketType
        //+ Name: string
        //+ BasePrice: double
        //+ GiftAidPrice: double                 
                                                  
        [Key]
        public int FareID { get; set; }

        [ForeignKey("FareType")]
        public virtual int FareTypeID { get; set; }
        public virtual FareType FareType { get; set; }

        [ForeignKey("TicketType")]
        public virtual int TicketTypeID { get; set; }
        public virtual TicketType TicketType { get; set; }

        [DisplayName("Basic Price")]
        [Required(ErrorMessage = "Fare must have a basic price")]

        public double BasicPrice { get; set; }

        [DisplayName("Gift Aid Price")]
        public double GiftAidPrice { get; set; }

        public Fare()
        {
            if (GiftAidPrice == 0)
            {
                GiftAidPrice = BasicPrice;
            }
        }

        public override string ToString()
        {
            return
                this.TicketType.Name
                + " - "
                + this.FareType.FareTypeDescription;
            //return "blah blah blah";
        }

        public string GetString()
        {
            return
                this.TicketType.Name
                + " - "
                + this.FareType.FareTypeDescription;
        }
    }
}