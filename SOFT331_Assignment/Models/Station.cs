using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SOFT331_Assignment.Models
{
    public class Station
    {
        [Key]
        public int StationID { get; set; }
        [Required]
        public string StationName { get; set; }
        [Required]
        public string StationDescription { get; set; }

        public Station()
        {

        }
    }
}
