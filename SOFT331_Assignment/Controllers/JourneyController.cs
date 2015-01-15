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
    public class JourneyController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Journey
        public ActionResult Index()
        {
            var journies = db.Journies.Include(j => j.ArrivalStation).Include(j => j.DepartureStation).Include(j => j.Train);
            return View(journies.ToList());
        }

        //private void getMonthDays

        // GET: Journey/Book/5
        public ActionResult Book(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } 

            //get all form data
            List<Fare> fares = db.Fares.Include(f => f.EventType).Include(f => f.FareType).ToList();
            Journey journey = db.Journies.Find(id);
            
            if (journey == null || fares == null)
            {
                return HttpNotFound();
            }
            
            var model = new BookViewModel 
            {
                Fares = fares,
                Traveller = new Traveller(),
                Journey = journey
            };

            return View(model);
        }

        // POST: Journey/Book
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book([Bind(Include = "JourneyID,TrainID,DepartureStationID,ArrivalStationID,DepartureTime,ArrivalTime,JourneyType,AdvanceTickets,NumberOfSeats")] Journey journey)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Journies.Add(journey);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.ArrivalStationID = new SelectList(db.Stations, "StationID", "StationName", journey.ArrivalStationID);
            //ViewBag.DepartureStationID = new SelectList(db.Stations, "StationID", "StationName", journey.DepartureStationID);
            //ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Name", journey.TrainID);
            //return View(journey);

            //work out which boxes are have values

            //save traveller

            //do something else

            return View();
        }

        // GET: Journey/Details/5
        public ActionResult Details(int? id)
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

        // GET: Journey/Create
        public ActionResult Create()
        {
            ViewBag.ArrivalStationID = new SelectList(db.Stations, "StationID", "StationName");
            ViewBag.DepartureStationID = new SelectList(db.Stations, "StationID", "StationName");
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Name");
            ViewBag.JourneyTypeID = new SelectList(db.JourneyTypes, "JourneyTypeID", "JourneyTypeName");
            return View();
        }

        // POST: Journey/Create
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
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Name", journey.TrainID);
            ViewBag.JourneyTypeID = new SelectList(db.JourneyTypes, "JourneyTypeID", "JourneyTypeName", journey.JourneyTypeID);
            return View(journey);
        }

        // GET: Journey/Edit/5
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
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Name", journey.TrainID);
            return View(journey);
        }

        // POST: Journey/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JourneyID,TrainID,DepartureStationID,ArrivalStationID,DepartureTime,ArrivalTime,JourneyType,AdvanceTickets,NumberOfSeats")] Journey journey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArrivalStationID = new SelectList(db.Stations, "StationID", "StationName", journey.ArrivalStationID);
            ViewBag.DepartureStationID = new SelectList(db.Stations, "StationID", "StationName", journey.DepartureStationID);
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Name", journey.TrainID);
            return View(journey);
        }

        // GET: Journey/Delete/5
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

        // POST: Journey/Delete/5
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
