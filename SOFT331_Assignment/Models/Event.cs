using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class Event
    {
        [Required]        
        public int EventID { get; set; }

        [Required(ErrorMessage="Event must have a name")]
        [DisplayName("Event Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Event must have a description")]
        [DisplayName("Event Description")]
        public string Description { get; set; }

        public Event()
        {
           
        }
    }
}