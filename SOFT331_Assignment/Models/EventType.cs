using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class EventType
    {
        [Key]
        public int EventTypeID { get; set; }
        
        [DisplayName("Event Name")]
        [Required]
        public string EventName { get; set; }
        
        [DisplayName("Event Description")]
        [Required]
        public string EventDescription { get; set; }

    }
}