using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class FareType
    {
        [Key]
        public int FaretypeID { get; set; }

        [DisplayName("Fare")]
        [Required]
        public string FareTypeDescription { get; set; }
               
        public FareType()
        {
      
        }
    }
}