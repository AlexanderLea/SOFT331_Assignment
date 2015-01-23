using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class TicketType
    {
        //+ TicketTypeID: int
        //+ Name: string
        //+ Description: string
        //+ DepartureStationID: int
        //+ DepartureStation: Station
        //+ ArrivalStationID: int
        //+ ArrivalStation: Station

        [Key]
        public int TicketTypeID { get; set; }

        [Required(ErrorMessage="Must have a ticket type name")]
        [DisplayName("Ticket Type Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Must have a ticket type description")]
        [DisplayName("Ticket Type Description")]
        public string Description { get; set; }

        public int? DepartureStationID { get; set; }
        public Station DepartureStation { get; set; }

        public int? ArrivalStationID { get; set; }
        public Station ArrivalStation { get; set; }

    }
}