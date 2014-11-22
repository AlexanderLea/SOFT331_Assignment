using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class Traveller
    {
        [Key]
        public int TravellerID { get; set; }

        //[Required(ErrorMessage="You must enter a first name")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "You must enter a last name")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "You must enter the first line of your address")]
        [DisplayName("Address Line 1")]
        public string Address1 { get; set; }
 
        //[Required(ErrorMessage = "You must enter a postcode")]
        [DisplayName("Postcode")]
        public string PostCode { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public Traveller()
        {

        }
    }
}