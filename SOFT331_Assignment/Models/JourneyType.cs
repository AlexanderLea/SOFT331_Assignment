using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class JourneyType
    {

        [Key]
        public int JourneyTypeID { get; set; }
        
        [Required]
        [DisplayName("Journey Type")]
        public string JourneyTypeName { get; set; }

        public ICollection<Journey> Journies { get; set; }
    }
}