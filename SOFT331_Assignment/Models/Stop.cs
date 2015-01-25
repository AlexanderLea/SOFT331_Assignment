using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class Stop
    {
        //+ StopID: int
        //+ StationID: int
        //+ Station: Station
        //+ JourneyID: int
        //+ Journey: Journey
        //+ NoOnwardSeats: int
        //+ ArrivalTime: DateTime?
        //+ DepartureTime: DateTime?
        //+ WheelchairBooked: bool
        //+ GetAvailableSeats(): int
        //+ CanBookWheelchair(): bool
        //+ BookTickets(_tickets: int): bool

        [Key]
        public int StopID { get; set; }

        [Required]
        public int StationID { get; set; }
        public virtual Station Station { get; set; }

        [Required]
        public int JourneyID { get; set; }
        public virtual Journey Journey { get; set; }

        [DisplayName("# Seats")]
        public int NoOnwardSeats { get; set; }
        
        [DisplayName("Arrival Date/Time")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time in the format dd/mm/yyyy hh:mm")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? ArrivalTime { get; set; }

        [DisplayName("Departure Date/Time")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time in the format dd/mm/yyyy hh:mm")]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DepartureTime { get; set; }

        [DisplayName("Has a wheelchair ticket been booked?")]
        public bool WheelchairBooked { get; set; }

        [Required]
        [DisplayName("Number of booked tickets")]
        public int NoBookedSeats { get; set; }

        public Stop()
        {
            //this.NoOnwardSeats = Journey.NumberOfSeats;
            this.WheelchairBooked = false;
        }

        public bool bookTicket(bool _wheelchair)
        {
            DatabaseContext db = new DatabaseContext();

            //TODO: replace with cout of journey.tickets
            if ((NoBookedSeats + 1) <= NoOnwardSeats)
            {
                NoBookedSeats ++;
                WheelchairBooked = _wheelchair;

                db.SaveChanges();

                return true;
            }
            else
                return false;
        }
    }
}