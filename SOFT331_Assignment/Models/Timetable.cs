using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFT331_Assignment.Models
{
    public class Timetable
    {
        public IEnumerable<Journey> Journeys { get; set; }
        public DateTime Date { get; set; }
        public Timetable()
        {

        }

        public Timetable(DateTime _date)
        {
            this.Date = _date;

            DatabaseContext db = new DatabaseContext();

            DateTime maxDate = _date.AddDays(1);

            //return valid journies
            this.Journeys = db.Journies.Where(j => j.DepartureTime >= _date && j.ArrivalTime < maxDate).ToList();
        }

        public bool bookTickets(int _noTickets)
        {
            bool success = false;

            foreach (Journey j in Journeys)
            {
                if (j.areTicketsAvailable(_noTickets))
                {
                    if (j.bookTickets(_noTickets))
                        success = true;
                    else
                        success = false;
                }
                else
                {
                    success = false;
                }
            }
            return success;
        }
    }
}