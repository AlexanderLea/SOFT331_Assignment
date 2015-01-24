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
