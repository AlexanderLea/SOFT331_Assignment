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

        [Required(ErrorMessage="Must have a fare type")]
        [DisplayName("Fare Type (e.g. Adult, Child, Student)")]        
        public string FareTypeDescription { get; set; }
               
        public FareType()
        {
      
        }
    }
}