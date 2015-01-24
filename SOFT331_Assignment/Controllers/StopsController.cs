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
    public class StopsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Stops
        public ActionResult Index()
        {
            var stops = db.Stops.Include(s => s.Journey).Include(s => s.Station);
            return View(stops.ToList());
        }

        // GET: Stops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stop stop = db.Stops.Find(id);
            if (stop == null)
            {
                return HttpNotFound();
            }
            return View(stop);
        }

        // GET: Stops/Create
        public ActionResult Create()
        {
            ViewBag.JourneyID = new SelectList(db.Journies, "JourneyID", "JourneyID");
            ViewBag.StationID = new SelectList(db.Stations, "StationID", "StationName");
            return View();
        }

        // POST: Stops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StopID,StationID,JourneyID,NoOnwardSeats,ArrivalTime,DepartureTime,WheelchairBooked")] Stop stop)
        {
            if (ModelState.IsValid)
            {
                db.Stops.Add(stop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JourneyID = new SelectList(db.Journies, "JourneyID", "JourneyID", stop.JourneyID);
            ViewBag.StationID = new SelectList(db.Stations, "StationID", "StationName", stop.StationID);
            return View(stop);
        }

        // GET: Stops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stop stop = db.Stops.Find(id);
            if (stop == null)
            {
                return HttpNotFound();
            }
            ViewBag.JourneyID = new SelectList(db.Journies, "JourneyID", "JourneyID", stop.JourneyID);
            ViewBag.StationID = new SelectList(db.Stations, "StationID", "StationName", stop.StationID);
            return View(stop);
        }

        // POST: Stops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StopID,StationID,JourneyID,NoOnwardSeats,ArrivalTime,DepartureTime,WheelchairBooked")] Stop stop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JourneyID = new SelectList(db.Journies, "JourneyID", "JourneyID", stop.JourneyID);
            ViewBag.StationID = new SelectList(db.Stations, "StationID", "StationName", stop.StationID);
            return View(stop);
        }

        // GET: Stops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stop stop = db.Stops.Find(id);
            if (stop == null)
            {
                return HttpNotFound();
            }
            return View(stop);
        }

        // POST: Stops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stop stop = db.Stops.Find(id);
            db.Stops.Remove(stop);
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
