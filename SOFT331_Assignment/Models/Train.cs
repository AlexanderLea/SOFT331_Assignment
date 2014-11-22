
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOFT331_Assignment.Models
{
    public class Train
    {
        #region instance variables
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

        public string Image { get; set; } //TODO:??

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

        [DisplayName("Driving Wheel Diameter")]
        public double DrivingWheelDiameter { get; set; }

        [DisplayName("Trailing Wheel Diameter")]
        public double TrailingWheelDiameter { get; set; }

        [DisplayName("Total Wheel Base")]
        public double TotalWheelbase { get; set; }

        [DisplayName("Cylinder size (bore/stroke)")]
        public double CylinderSize { get; set; }

        [DisplayName("Heating surface (sq. ft)")]
        public double HeatingSurface { get; set; }

        [DisplayName("Working pressure (psi)")]
        public double WorkingPressure { get; set; }

        [DisplayName("Tractive effort @ 85% BP (lbs)")]
        public double TractiveEffort { get; set; }

        [DisplayName("Weight (tons)")]
        public double Weight { get; set; }

        [DisplayName("Length over buffers")]
        public double LengthOverBuffers { get; set; }

        [DisplayName("Donor Locomotive")]
        public string DonorLoco { get; set; }

        #endregion

        //default constructor
        public Train()
        {

        }

        public override string ToString()
        {
            return "No." + this.TrainNumber + " " + this.Name;
        }

        

    }
}