using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOFT331_Assignment.Models;

namespace SOFT331_Assignment.Controllers
{
    public class TimetableController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Timetable
        public ActionResult Index()
        {
            var journies = db.Journies.Include(j => j.ArrivalStation).Include(j => j.DepartureStation).Include(j => j.JourneyType).Include(j => j.Train);
            return View(journies.ToList());
        }

        // GET: Timetable/Details/5
        public ActionResult Details(string year, string month, string day)
        {
            if (year == null || month == null || day == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Construct date objects for searching
            DateTime minDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day), 0, 0, 0, 0);
            DateTime maxDate = minDate.AddDays(1);

            //return valid journies
            var journies = db.Journies.Where(j => j.DepartureTime >= minDate && j.ArrivalTime < maxDate);

            if (journies == null)
            {
                return HttpNotFound();
            }

            ViewBag.Date = minDate.ToShortDateString();

            return View(journies.ToList());
        }

        // GET: Timetable/Create
        public ActionResult Create()
        {
            ViewBag.ArrivalStationID = new SelectList(db.Stations, "StationID", "StationName");
            ViewBag.DepartureStationID = new SelectList(db.Stations, "StationID", "StationName");
            ViewBag.JourneyTypeID = new SelectList(db.JourneyTypes, "JourneyTypeID", "JourneyTypeName");
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Name");
            return View();
        }

        // POST: Timetable/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JourneyID,TrainID,DepartureStationID,ArrivalStationID,DepartureTime,ArrivalTime,JourneyTypeID,AdvanceTickets,NumberOfSeats")] Journey journey)
        {
            if (ModelState.IsValid)
            {
                db.Journies.Add(journey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArrivalStationID = new SelectList(db.Stations, "StationID", "StationName", journey.ArrivalStationID);
            ViewBag.DepartureStationID = new SelectList(db.Stations, "StationID", "StationName", journey.DepartureStationID);
            ViewBag.JourneyTypeID = new SelectList(db.JourneyTypes, "JourneyTypeID", "JourneyTypeName", journey.JourneyTypeID);
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Name", journey.TrainID);
            return View(journey);
        }

        // GET: Timetable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journey journey = db.Journies.Find(id);
            if (journey == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArrivalStationID = new SelectList(db.Stations, "StationID", "StationName", journey.ArrivalStationID);
            ViewBag.DepartureStationID = new SelectList(db.Stations, "StationID", "StationName", journey.DepartureStationID);
            ViewBag.JourneyTypeID = new SelectList(db.JourneyTypes, "JourneyTypeID", "JourneyTypeName", journey.JourneyTypeID);
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Name", journey.TrainID);
            return View(journey);
        }

        // POST: Timetable/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JourneyID,TrainID,DepartureStationID,ArrivalStationID,DepartureTime,ArrivalTime,JourneyTypeID,AdvanceTickets,NumberOfSeats")] Journey journey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArrivalStationID = new SelectList(db.Stations, "StationID", "StationName", journey.ArrivalStationID);
            ViewBag.DepartureStationID = new SelectList(db.Stations, "StationID", "StationName", journey.DepartureStationID);
            ViewBag.JourneyTypeID = new SelectList(db.JourneyTypes, "JourneyTypeID", "JourneyTypeName", journey.JourneyTypeID);
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Name", journey.TrainID);
            return View(journey);
        }

        // GET: Timetable/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journey journey = db.Journies.Find(id);
            if (journey == null)
            {
                return HttpNotFound();
            }
            return View(journey);
        }

        // POST: Timetable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Journey journey = db.Journies.Find(id);
            db.Journies.Remove(journey);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
