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
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Traveller);
            return View(tickets.ToList());
        }

        public ActionResult Book(string year, string month, string day)
        {
            if (year == null || month == null || day == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Construct date objects for searching
            DateTime minDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day), 0, 0, 0, 0);
            DateTime maxDate = minDate.AddDays(1);

            //return valid journies
            IEnumerable<Journey> journies = db.Journies.Where(j => j.DepartureTime >= minDate && j.ArrivalTime < maxDate);
            List<Fare> fares = db.Fares.Include(f => f.EventType).Include(f => f.FareType).ToList(); 

            if (journies == null || fares == null)
            {
                return HttpNotFound();
            }            

            //create collection for combined view
            var model = new BookTicketModel
            {
                Fares = fares,
                Traveller = new Traveller(),
                Journeys = journies
            };

            ViewBag.Date = minDate.ToShortDateString();                                  

            return View(model);
        }

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

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.TravellerID = new SelectList(db.Travellers, "TravellerID", "FirstName");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketID,TravellerID,TicketDate,FareType,EventType,TicketPrice")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TravellerID = new SelectList(db.Travellers, "TravellerID", "FirstName", ticket.TravellerID);
            return View(ticket);
        }

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
            ViewBag.TravellerID = new SelectList(db.Travellers, "TravellerID", "FirstName", ticket.TravellerID);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketID,TravellerID,TicketDate,FareType,EventType,TicketPrice")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TravellerID = new SelectList(db.Travellers, "TravellerID", "FirstName", ticket.TravellerID);
            return View(ticket);
        }

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
