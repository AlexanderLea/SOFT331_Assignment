using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SOFT331_Assignment.Models
{
    public class Station
    {
        [Key]
        public int StationID { get; set; }
        
        [DisplayName("Station Name")]
        [Required]
        public string StationName { get; set; }
        [Required]
        [DisplayName("Station Description")]
        public string StationDescription { get; set; }
        
        public virtual ICollection<Journey> arrivalJournies { get; set; }
        public virtual ICollection<Journey> departureJournies { get; set; }

        public Station()
        {

        }
    }
}
