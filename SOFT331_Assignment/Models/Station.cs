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
        //+ StationID: int
        //+ Name: string
        //+ Description: string
        //+ WheelchairAccessible: bool
        //+ RequestStop: bool
        //+ Carpark: bool
        //+ RefreshmentsAvailable: bool
        //+ ToiletAvailable: bool
        //+ MainlineStationNear: bool

        [Key]
        public int StationID { get; set; }
        
        [Required]
        [DisplayName("Station Name")]
        public string StationName { get; set; }

        [Required]
        [DisplayName("Station Description")]
        public string StationDescription { get; set; }        

        [DisplayName("Request Stop")]
        public bool RequestStop { get; set; }

        [DisplayName("Wheelchair Accessible")]
        public bool WheelchairAccessible { get; set; }

        [DisplayName("Car Park Available")]
        public bool CarPark { get; set; }

        [DisplayName("Refreshments Available")]
        public bool RefreshmentsAvailable { get; set; }

        [DisplayName("Toilets Available")]
        public bool ToiletAvailable { get; set; }

        [DisplayName("Mainline Station Near")]
        public bool MainlineStationNear { get; set; }

        public virtual ICollection<TicketGroup> arrivalTicketTypes { get; set; }
        public virtual ICollection<TicketGroup> departureTicketTypes { get; set; }

        public Station()
        {
            this.WheelchairAccessible = false;
            this.RequestStop = false;
            this.CarPark = false;
            this.RefreshmentsAvailable = false;
            this.ToiletAvailable = false;
            this.MainlineStationNear = false;            
        }
    }
}
