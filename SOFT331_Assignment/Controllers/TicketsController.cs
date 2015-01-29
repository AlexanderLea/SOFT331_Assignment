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
            ViewData["Fares"] = db.Fares.ToList();

            ViewBag.TicketGroup = new SelectList(db.TicketTypes, "TicketTypeID", "Name");
            ViewBag.FareID = new SelectList(db.Fares, "FareID", "FareID");
            ViewBag.JourneyID = new SelectList(db.Journies, "JourneyID", "JourneyID");

            return View();
        }

        // GET: Tickets
        public ActionResult Confirm()
        {
            ViewData["Ticket"] = TempData["Ticket"];
            ViewData["Traveller"] = TempData["Traveller"];
            TempData.Keep();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm([Bind()] Object xyz)
        {
            if (ModelState.IsValid)
            {
                //get traveller
                Traveller t = (Traveller)TempData["Traveller"];
                //get ticket
                Ticket ti = (Ticket)TempData["Ticket"];
                int tID = -1;
                if (t != null)
                {
                    //save traveller
                    tID = t.save();
                }

                //check ticket exists
                if (ti != null)
                {
                    //Is Traveller ID null???
                    if (tID > 0)
                    {
                        ti.TravellerID = tID;
                    }

                    if (ti.book())
                    {
                        ti.Journey = null;
                        ti.Fare = null;

                        db.Tickets.Add(ti);
                        db.SaveChanges();
                        return RedirectToAction("Success", new { id = ti.TicketID });
                    }
                    else
                    {
                        ModelState.AddModelError("Cannot book ticket", "There was an error processing your booking, and it cannot be completed at this time.");
                    }
                }
            }

            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketID,FareID,JourneyID,GiftAid,Wheelchair,Carer")] Ticket ticket)
        {
            //All the includes counteract lazy loading
            ticket.Fare = db.Fares
                .Include(f => f.TicketType)
                .Include(f => f.TicketType.ArrivalStation)
                .Include(f => f.TicketType.DepartureStation)
                .Include(f => f.FareType)
                .Where(f => f.FareID == ticket.FareID)
                .First();

            ticket.Journey = db.Journies
                .Include(t => t.Tickets)
                .Include(t => t.Stops)
                .Include(t => t.Event)
                .FirstOrDefault();

            ticket.Journey.Stops = db.Stops
                .Where(s => s.JourneyID == ticket.JourneyID)
                .Include(s => s.Station)
                .ToList();

            //save ticket
            if (ticket.Wheelchair)
            {
                if (!ticket.Journey.canBookWheelchair(ticket.Fare.TicketType.DepartureStation, ticket.Fare.TicketType.ArrivalStation))
                {
                    ModelState.AddModelError("Cannot book wheelchair", "Unfortunately, a wheelchair has already been booked on this train, and there can only be one wheelchair at a time.");
                }
            }


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

                ticket.Journey = db.Journies
                    .Include(t => t.Tickets)
                    .Include(t => t.Stops)
                    .Include(t => t.Event)
                    .FirstOrDefault();

                ticket.Journey.Stops = db.Stops
                    .Where(s => s.JourneyID == ticket.JourneyID)
                    .Include(s => s.Station)
                    .ToList();

                if (ticket.Journey.getJourneyDate() > DateTime.Today)
                {
                    if (ticket.Carer)
                    {
                        ticket.Fare.GiftAidPrice = ticket.Fare.GiftAidPrice * 0.8;
                        ticket.Fare.BasicPrice = ticket.Fare.BasicPrice * 0.8;
                    }

                    if (ticket.GiftAid)
                    {
                        //redirect to create traveller
                        TempData["Ticket"] = ticket;
                        return RedirectToAction("Create", "Travellers");
                    }
                    else
                    {
                        TempData["Ticket"] = ticket;
                        return RedirectToAction("Confirm");
                    }
                }
                else
                {
                    ModelState.AddModelError("", String.Format("You cannot book a ticket for today!"));
                    return View();
                }
                //else
                //ERROR                
            }
            //ModelState.AddModelError("", String.Format("You have not filled in all the fields!"));

            ViewData["Journey"] = ticket.Journey;
            ViewData["Fares"] = db.Fares.ToList();

            ViewBag.TicketGroup = new SelectList(db.TicketTypes, "TicketTypeID", "Name");
            ViewBag.FareID = new SelectList(db.Fares, "FareID", "FareID");
            ViewBag.JourneyID = new SelectList(db.Journies, "JourneyID", "JourneyID");
            return View();
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

        // GET: Tickets/Success/5
        public ActionResult Success(int? id)
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

            ticket.Fare = db.Fares
                    .Include(f => f.TicketType)
                    .Include(f => f.TicketType.ArrivalStation)
                    .Include(f => f.TicketType.DepartureStation)
                    .Include(f => f.FareType)
                    .Where(f => f.FareID == ticket.FareID)
                    .First();

            ticket.Journey = db.Journies
                .Include(t => t.Tickets)
                .Include(t => t.Stops)
                .Include(t => t.Event)
                .FirstOrDefault();

            ticket.Journey.Stops = db.Stops
                .Where(s => s.JourneyID == ticket.JourneyID)
                .Include(s => s.Station)
                .ToList();

            //delete from models
            ticket.delete();

            //delete from DB
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
