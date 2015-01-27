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
    public class TicketsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Tickets
        [Authorize(Roles = "CLERK, ADMIN")]
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Fare).Include(t => t.Traveller);
            return View(tickets.ToList());
        }

        [Authorize(Roles = "CLERK, ADMIN")]
        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create/JourneyID
        public ActionResult Create(int? id)//string year, string month, string day)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journey journey = db.Journies.Find(id);

            if (journey == null)
            {
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return View();
            }

            ViewData["Journey"] = journey;

            ViewBag.FareID = new SelectList(db.Fares, "FareID", "FareID");
            ViewBag.TravellerID = new SelectList(db.Travellers, "TravellerID", "FirstName");
            ViewBag.JourneyID = new SelectList(db.Journies, "JourneyID", "JourneyID");

            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketID,TravellerID,FareID,JourneyID,GiftAid,Wheelchair,Carer")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                //All the includes counteract lazy loading
                ticket.Fare = db.Fares
                    .Include(f => f.TicketType)
                    .Include(f => f.TicketType.ArrivalStation)
                    .Include(f => f.TicketType.DepartureStation)
                    .Include(f => f.FareType)
                    .Where(f => f.FareID == ticket.FareID)
                    .First();

                ticket.Journey = db.Journies.Find(ticket.JourneyID);
                ticket.Traveller = db.Travellers.Find(ticket.TravellerID);

                if (ticket.book())
                {
                    db.Tickets.Add(ticket);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //else
                //ERROR                
            }

            ViewBag.FareID = new SelectList(db.Fares, "FareID", "FareID", ticket.FareID);
            ViewBag.TravellerID = new SelectList(db.Travellers, "TravellerID", "FirstName", ticket.TravellerID);
            return View(ticket);
        }

        [Authorize(Roles = "CLERK, ADMIN")]
        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.FareID = new SelectList(db.Fares, "FareID", "FareID", ticket.FareID);
            ViewBag.TravellerID = new SelectList(db.Travellers, "TravellerID", "FirstName", ticket.TravellerID);
            return View(ticket);
        }

        [Authorize(Roles = "CLERK, ADMIN")]
        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketID,TravellerID,FareID,GiftAid,Wheelchair,Carer")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FareID = new SelectList(db.Fares, "FareID", "FareID", ticket.FareID);
            ViewBag.TravellerID = new SelectList(db.Travellers, "TravellerID", "FirstName", ticket.TravellerID);
            return View(ticket);
        }

        [Authorize(Roles = "CLERK, ADMIN")]
        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        [Authorize(Roles = "CLERK, ADMIN")]
        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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
