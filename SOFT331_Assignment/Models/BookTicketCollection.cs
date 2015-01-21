using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class BookTicketCollection
    {
        public IEnumerable<Journey> Journeys { get; set; }
        public Traveller Traveller { get; set; }
        public IEnumerable<Fare> Fares { get; set; }
    }
}