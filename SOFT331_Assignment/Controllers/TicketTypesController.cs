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
    public class TicketTypesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: TicketTypes
        public ActionResult Index()
        {
            var ticketTypes = db.TicketTypes.Include(t => t.ArrivalStation).Include(t => t.DepartureStation);
            return View(ticketTypes.ToList());
        }

        // GET: TicketTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketGroup ticketType = db.TicketTypes.Find(id);
            if (ticketType == null)
            {
                return HttpNotFound();
            }
            return View(ticketType);
        }

        // GET: TicketTypes/Create
        public ActionResult Create()
        {
            ViewBag.ArrivalStationID = new SelectList(db.Stations, "StationID", "StationName");
            ViewBag.DepartureStationID = new SelectList(db.Stations, "StationID", "StationName");
            return View();
        }

        // POST: TicketTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketTypeID,Name,Description,DepartureStationID,ArrivalStationID")] TicketGroup ticketType)
        {
            if (ModelState.IsValid)
            {
                db.TicketTypes.Add(ticketType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArrivalStationID = new SelectList(db.Stations, "StationID", "StationName", ticketType.ArrivalStationID);
            ViewBag.DepartureStationID = new SelectList(db.Stations, "StationID", "StationName", ticketType.DepartureStationID);
            return View(ticketType);
        }

        // GET: TicketTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketGroup ticketType = db.TicketTypes.Find(id);
            if (ticketType == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArrivalStationID = new SelectList(db.Stations, "StationID", "StationName", ticketType.ArrivalStationID);
            ViewBag.DepartureStationID = new SelectList(db.Stations, "StationID", "StationName", ticketType.DepartureStationID);
            return View(ticketType);
        }

        // POST: TicketTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketTypeID,Name,Description,DepartureStationID,ArrivalStationID")] TicketGroup ticketType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArrivalStationID = new SelectList(db.Stations, "StationID", "StationName", ticketType.ArrivalStationID);
            ViewBag.DepartureStationID = new SelectList(db.Stations, "StationID", "StationName", ticketType.DepartureStationID);
            return View(ticketType);
        }

        // GET: TicketTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketGroup ticketType = db.TicketTypes.Find(id);
            if (ticketType == null)
            {
                return HttpNotFound();
            }
            return View(ticketType);
        }

        // POST: TicketTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketGroup ticketType = db.TicketTypes.Find(id);
            db.TicketTypes.Remove(ticketType);
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
