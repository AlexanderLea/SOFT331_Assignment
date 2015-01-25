using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class BookTicketCollection
    {
        public Ticket Ticket { get; set; }
        public Journey Journey { get; set; }
        public Traveller Traveller { get; set; }
        public IEnumerable<Fare> Fares { get; set; }
    }
}