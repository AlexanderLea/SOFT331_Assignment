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
    public class FareController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Fare
        public ActionResult Index()
        {
            var fares = db.Fares.Include(f => f.EventType).Include(f => f.FareType);
            return View(fares.ToList());
        }

        // GET: Fare/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fare fare = db.Fares.Find(id);
            if (fare == null)
            {
                return HttpNotFound();
            }
            return View(fare);
        }

        // GET: Fare/Create
        public ActionResult Create()
        {
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventName");
            ViewBag.FareTypeID = new SelectList(db.FareTypes, "FaretypeID", "FareTypeDescription");
            return View();
        }

        // POST: Fare/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FareID,Description,FareTypeID,EventTypeID,BasicPrice,GiftAidPrice")] Fare fare)
        {
            if (ModelState.IsValid)
            {
                db.Fares.Add(fare);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventName", fare.EventTypeID);
            ViewBag.FareTypeID = new SelectList(db.FareTypes, "FaretypeID", "FareTypeDescription", fare.FareTypeID);
            return View(fare);
        }

        // GET: Fare/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fare fare = db.Fares.Find(id);
            if (fare == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventName", fare.EventTypeID);
            ViewBag.FareTypeID = new SelectList(db.FareTypes, "FaretypeID", "FareTypeDescription", fare.FareTypeID);
            return View(fare);
        }

        // POST: Fare/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FareID,Description,FareTypeID,EventTypeID,BasicPrice,GiftAidPrice")] Fare fare)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fare).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "EventTypeID", "EventName", fare.EventTypeID);
            ViewBag.FareTypeID = new SelectList(db.FareTypes, "FaretypeID", "FareTypeDescription", fare.FareTypeID);
            return View(fare);
        }

        // GET: Fare/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fare fare = db.Fares.Find(id);
            if (fare == null)
            {
                return HttpNotFound();
            }
            return View(fare);
        }

        // POST: Fare/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fare fare = db.Fares.Find(id);
            db.Fares.Remove(fare);
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
