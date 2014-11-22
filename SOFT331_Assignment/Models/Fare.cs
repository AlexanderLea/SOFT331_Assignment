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

        [ForeignKey("FareType")]
        public virtual int FareTypeID { get; set; }
        public virtual FareType FareType { get; set; }

        [ForeignKey("EventType")]
        public virtual int EventTypeID { get; set; }
        public virtual EventType EventType { get; set; }

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
    }
}