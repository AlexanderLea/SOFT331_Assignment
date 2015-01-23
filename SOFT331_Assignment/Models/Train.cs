
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOFT331_Assignment.Models
{
    public class Train
    {
        //+ TrainID: int
        //+ Number: int
        //+ Name: int
        //+ Description: string
        //+ Year: int
        //+ WorksNo: int
        //+ Make: string
        //+ Type: string

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrainID { get; set; }

        [DisplayName("Train Number")]
        [Required(ErrorMessage="Train must have a number!")]
        public int TrainNumber { get; set; }

        [DisplayName("Train Name")]
        [Required(ErrorMessage = "Train must have a name")]
        public string Name { get; set; }

        [DisplayName("Train Description")]
        [Required(ErrorMessage = "Train must have a description")]        
        public string Description { get; set; }

        [DisplayName("Train Maker")]
        public string Maker { get; set; }

        [DisplayName("Year of Manufacture")]
        [Required(ErrorMessage = "Train must have a maker")]
        public string Year { get; set; }

        [DisplayName("Works Number")]
        [Required(ErrorMessage = "Train must have a works number")]
        public int WorksNumber { get; set; }
        
        [DisplayName("Train Type")]
        public string Type { get; set; }

        //default constructor
        public Train()
        {

        }

        public override string ToString()
        {
            return "No." + this.TrainNumber 
                + " " 
                + this.Name
                + " - "
                + this.Maker
                + ", "
                + this.Year;
        }

        

    }
}