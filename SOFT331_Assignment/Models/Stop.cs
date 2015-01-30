using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class Stop
    {   
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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? ArrivalTime { get; set; }

        [DisplayName("Departure Date/Time")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time in the format dd/mm/yyyy hh:mm")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
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

        /// <summary>
        /// Books a ticket for this stop
        /// </summary>
        /// <param name="_wheelchair">Is the ticket booked a wheelchair ticket</param>
        /// <returns>Boolean based on success of booking ticket</returns>
        public bool bookTicket(bool _wheelchair)
        {
            this.Station = null;
            this.Journey = null;
            
            DatabaseContext db = new DatabaseContext();
           
            if ((NoBookedSeats + 1) <= NoOnwardSeats)
            {
                this.NoBookedSeats ++;
                this.WheelchairBooked = _wheelchair;

                var temp = this;

                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Deletes ticket from stop
        /// </summary>
        /// <param name="_wheelchair">Does ticket to be deleted have a wheelchair?</param>
        internal void deleteTicket(bool _wheelchair)
        {
            DatabaseContext db = new DatabaseContext();

            if ((NoBookedSeats + 1) <= NoOnwardSeats)
            {
                this.NoBookedSeats--;
                if(_wheelchair)
                    this.WheelchairBooked = (!_wheelchair);

                var temp = this;

                //db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}