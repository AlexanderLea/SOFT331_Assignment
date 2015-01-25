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
            var tickets = db.Tickets.Include(t => t.Fare).Include(t => t.Traveller);
            return View(tickets.ToList());
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

        // GET: Tickets/Create/JourneyID
        public ActionResult Create(int? id)//string year, string month, string day)
        {
            //Construct date objects for searching
            //DateTime minDate = DateTime.MinValue;
            //DateTime maxDate = DateTime.MaxValue;

            //if (year == null && month == null && day == null)
            //{
            //    //show everything
            //    minDate = DateTime.MinValue;
            //} 
            //else if (month == null && day == null)
            //{ 
            //    //show year 
            //    minDate = new DateTime(Convert.ToInt32(year), 1, 1);
            //    maxDate = minDate.AddYears(1);
            //}
            //else if (day == null)
            //{
            //    //show year and month
            //    minDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);
            //    maxDate = minDate.AddMonths(1);
            //}
            //else
            //{
            //    //show everything
            //    minDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
            //    maxDate = minDate.AddDays(1);
            //}

            ////return valid journies
            //List<Journey> journies = db.Journies.ToList(); //.Where(j => j.Stops. >= minDate && j.ArrivalTime < maxDate)

            //List<Fare> fares = db.Fares.ToList();

            //if (journies.Count() < 1 || fares.Count() < 1)
            //{
            //    return new HttpStatusCodeResult(204);
            //}

            ////create collection for combined view
            //var model = new BookTicketCollection
            //{
            //    Fares = fares,
            //    Traveller = new Traveller(),
            //    Journeys = journies
            //};

            //ViewBag.MinDate = minDate;
            //ViewBag.MaxDate = maxDate;

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
