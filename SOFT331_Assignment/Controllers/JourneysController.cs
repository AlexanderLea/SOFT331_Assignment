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
    public class JourneysController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Journeys
        public ActionResult Timetable(string year, string month, string day)
        {
            //Construct date objects for searching
            DateTime minDate = DateTime.MinValue;
            DateTime maxDate = DateTime.MaxValue;

            if (year == null && month == null && day == null)
            {
                //show everything
                minDate = DateTime.MinValue;
            }
            else if (month == null && day == null)
            {
                //show year 
                minDate = new DateTime(Convert.ToInt32(year), 1, 1);
                maxDate = minDate.AddYears(1);
            }
            else if (day == null)
            {
                //show year and month
                minDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);
                maxDate = minDate.AddMonths(1);
            }
            else
            {
                //show everything
                minDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
                maxDate = minDate.AddDays(1);
            }

            //return valid journies
            ViewData["Journeys"] = db.Journies.ToList();//.Where(j => j.Stops >= minDate && j.ArrivalTime < maxDate)
            List<Fare> fares = db.Fares.ToList();

            ViewBag.MinDate = minDate;
            ViewBag.MaxDate = maxDate;
            //if (journies.Count() < 1 || fares.Count() < 1)
            //{
            //    return new HttpStatusCodeResult(204);
            //}

            return View();
        }

        // GET: Journeys
        public ActionResult Index()
        {
            var journies = db.Journies.Include(j => j.Event).Include(j => j.Train);
            return View(journies.ToList());
        }

        // GET: Journeys/Details/5
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

        // GET: Journeys/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Name");
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Name");
            return View();
        }

        // POST: Journeys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JourneyID,TrainID,EventID,AdvanceTickets,FirstClassTickets,NumberOfSeats")] Journey journey)
        {
            if (ModelState.IsValid)
            {
                db.Journies.Add(journey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "EventID", "Name", journey.EventID);
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Name", journey.TrainID);
            return View(journey);
        }

        // GET: Journeys/Edit/5
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
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Name", journey.EventID);
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Name", journey.TrainID);
            return View(journey);
        }

        // POST: Journeys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JourneyID,TrainID,EventID,AdvanceTickets,FirstClassTickets,NumberOfSeats")] Journey journey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Name", journey.EventID);
            ViewBag.TrainID = new SelectList(db.Trains, "TrainID", "Name", journey.TrainID);
            return View(journey);
        }

        // GET: Journeys/Delete/5
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

        // POST: Journeys/Delete/5
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
